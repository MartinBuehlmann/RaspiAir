namespace RaspiAir.Measurement.SCD41;

using Meadow;
using Meadow.Foundation.Sensors.Environmental;
using Meadow.Hardware;
using RaspiAir.Common.Logging;

internal class Sensor : ISensor
{
    private readonly Log logger;
    private Scd41? sensor;

    public Sensor(Log logger)
    {
        this.logger = logger;
    }

    public event Action<SensorData>? OnDataReceived;

    public void Start()
    {
        II2cBus i2cBus = new RaspberryPi().CreateI2cBus();
        this.sensor = new Scd41(i2cBus);

        var serial = BitConverter.ToString(this.sensor.GetSerialNumber());
        this.logger.Info("SCD41 Serial: {Serial}", serial);

        var consumer = Scd41.CreateObserver(
            handler: result =>
            {
                this.logger.Info(
                    "Observer: Temp changed by threshold; new temp: {NewTemperature:N2}C, old: {OldTemperature:N2}C",
                    result.New.Temperature?.Celsius!,
                    result.Old?.Temperature?.Celsius!);
            },
            filter: result =>
            {
                if (result is
                    {
                        Old: { Temperature: { } oldTemp, Humidity: { } oldHumidity },
                        New: { Temperature: { } newTemp, Humidity: { } newHumidity }
                    })
                {
                    return (newTemp - oldTemp).Abs().Celsius > 0.5 &&
                           (newHumidity - oldHumidity).Percent > 0.05;
                }

                return false;
            });

        if (this.sensor != null)
        {
            this.sensor.Subscribe(consumer);

            this.sensor.Updated += (_, result) =>
            {
                this.logger.Info("Concentration: {Concentration:N0}ppm", result.New.Concentration?.PartsPerMillion!);
                this.logger.Info("Temperature: {Temperature:N1}C", result.New.Temperature?.Celsius!);
                this.logger.Info("Relative Humidity: {Humidity:N0}%", result.New.Humidity!);

                this.OnDataReceived?.Invoke(
                    new SensorData(
                        (int)result.New.Concentration?.PartsPerMillion!,
                        (double)result.New.Temperature?.Celsius!,
                        result.New.Humidity!.Value.Percent));
            };

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
}