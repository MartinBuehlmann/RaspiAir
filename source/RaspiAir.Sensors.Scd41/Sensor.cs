namespace RaspiAir.Sensors.Scd41;

using System;
using Meadow;
using Meadow.Hardware;
using Meadow.Units;
using RaspiAir.Common.Logging;

internal class Sensor : ISensor
{
    private readonly Log logger;
    private Meadow.Foundation.Sensors.Environmental.Scd41? sensor;

    public Sensor(Log logger)
    {
        this.logger = logger;
    }

    public event Action<double>? OnTemperatureChanged;

    public event Action<double>? OnHumidityChanged;

    public event Action<double>? OnCo2ConcentrationChanged;

    public void Start()
    {
        II2cBus i2CBus = new RaspberryPi().CreateI2cBus();
        this.sensor = new Meadow.Foundation.Sensors.Environmental.Scd41(i2CBus);

        var serial = BitConverter.ToString(this.sensor.GetSerialNumber());
        this.logger.Info("SCD41 Serial: {Serial}", serial);

        var temperatureConsumer = this.CreateObserver(
            x => x.New.Temperature?.Celsius,
            x => x.Old?.Temperature?.Celsius,
            x => this.OnTemperatureChanged?.Invoke(x));

        var humidityConsumer = this.CreateObserver(
            x => x.New.Humidity?.Percent,
            x => x.Old?.Humidity?.Percent,
            x => this.OnHumidityChanged?.Invoke(x));

        var concentrationConsumer = this.CreateObserver(
            x => x.New.Concentration?.PartsPerMillion,
            x => x.Old?.Concentration?.PartsPerMillion,
            x => this.OnCo2ConcentrationChanged?.Invoke(x),
            1);

        if (this.sensor != null)
        {
            this.sensor.Subscribe(temperatureConsumer);
            this.sensor.Subscribe(humidityConsumer);
            this.sensor.Subscribe(concentrationConsumer);
            this.sensor.StartUpdating(TimeSpan.FromSeconds(6));
        }
    }

    public void Stop()
    {
        if (this.sensor == null)
        {
            throw new InvalidOperationException("Sensor does not exist or has not been started.");
        }

        this.sensor.StopUpdating();
    }

    private FilterableChangeObserver<
            (Concentration? Concentration, Temperature? Temperature, RelativeHumidity? Humidity)>
        CreateObserver(
            Func<IChangeResult<(Concentration? Concentration, Temperature? Temperature, RelativeHumidity? Humidity)>,
                double?> newValueSelector,
            Func<IChangeResult<(Concentration? Concentration, Temperature? Temperature, RelativeHumidity? Humidity)>,
                double?> oldValueSelector,
            Action<double>? onChangedHandler,
            double threshold = 0.1)
    {
        return Meadow.Foundation.Sensors.Environmental.Scd41.CreateObserver(
            handler: result => { onChangedHandler?.Invoke(newValueSelector(result)!.Value); },
            filter: result =>
            {
                if (oldValueSelector(result) is null || newValueSelector(result) is null)
                {
                    return false;
                }

                double newValue = newValueSelector(result)!.Value;
                double oldValue = oldValueSelector(result)!.Value;
                return Math.Abs(newValue - oldValue) >= threshold;
            });
    }
}