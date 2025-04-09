namespace RaspiAir.Sensors.Scd41;

using System;
using AppServices.Common;
using AppServices.Common.Logging;
using Meadow;
using Meadow.Hardware;
using Meadow.Units;

internal class Scd41Sensor(Log logger) : ISensor
{
    private static readonly TimeSpan MeasurementInterval = TimeSpan.FromSeconds(60);
    private Meadow.Foundation.Sensors.Environmental.Scd41? sensor;

    public event EventHandler<EventArgs<double>>? TemperatureChanged;

    public event EventHandler<EventArgs<double>>? HumidityChanged;

    public event EventHandler<EventArgs<int>>? Co2ConcentrationChanged;

    public void StartSensor()
    {
        II2cBus i2CBus = new RaspberryPi().CreateI2cBus();
        this.sensor = new Meadow.Foundation.Sensors.Environmental.Scd41(i2CBus);

        var serial = BitConverter.ToString(this.sensor.GetSerialNumber());
        logger.Info("SCD41 Serial: {Serial}", serial);

        var temperatureConsumer = CreateObserver(
            x => x.New.Temperature?.Celsius,
            x => x.Old?.Temperature?.Celsius,
            x => this.TemperatureChanged?.Invoke(this, new EventArgs<double>(x)));

        var humidityConsumer = CreateObserver(
            x => x.New.Humidity?.Percent,
            x => x.Old?.Humidity?.Percent,
            x => this.HumidityChanged?.Invoke(this, new EventArgs<double>(x)));

        var concentrationConsumer = CreateObserver(
            x => x.New.Concentration?.PartsPerMillion,
            x => x.Old?.Concentration?.PartsPerMillion,
            x => this.Co2ConcentrationChanged?.Invoke(this, new EventArgs<int>((int)x)),
            1);

        if (this.sensor != null)
        {
            this.sensor.Subscribe(temperatureConsumer);
            this.sensor.Subscribe(humidityConsumer);
            this.sensor.Subscribe(concentrationConsumer);
            this.sensor.StartUpdating(MeasurementInterval);
        }
    }

    public void StopSensor()
    {
        if (this.sensor == null)
        {
            throw new InvalidOperationException("Sensor does not exist or has not been started.");
        }

        this.sensor.StopUpdating();
    }

    private static FilterableChangeObserver<
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