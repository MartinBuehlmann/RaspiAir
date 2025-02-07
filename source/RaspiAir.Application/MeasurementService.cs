using RaspiAir.Measurement;

namespace RaspiAir.Application;

using RaspiRobot.Lights.Common;

internal class MeasurementService : IBackgroundService
{
    private readonly ISensor sensor;

    public MeasurementService(ISensor sensor)
    {
        this.sensor = sensor;
    }

    public int Order => 1;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.sensor.Start();
        this.sensor.OnDataReceived += this.HandleSensorDataReceived;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.sensor.OnDataReceived -= this.HandleSensorDataReceived;
        return Task.CompletedTask;
    }

    private void HandleSensorDataReceived(SensorData sensorData)
    {
        Console.WriteLine($"Temperature: {sensorData.Celsius:N2}C");
        Console.WriteLine($"Humidity   : {sensorData.Humidity:N1}%");
        Console.WriteLine($"CO2        : {sensorData.PartsPerMillion}ppm");
    }
}