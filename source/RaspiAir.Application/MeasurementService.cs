namespace RaspiAir.Application;

using RaspiAir.Measurement;
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
        this.sensor.Stop();
        return Task.CompletedTask;
    }

    private void HandleSensorDataReceived(SensorData sensorData)
    {
        Console.WriteLine($"Temperature: {sensorData.Celsius:N2}C");

        Console.WriteLine($"Humidity   : {sensorData.Humidity:N1}%");

        Console.Write("CO2        : ");
        Console.ForegroundColor =
            sensorData.PartsPerMillion < 400 ? ConsoleColor.DarkGreen :
            sensorData.PartsPerMillion < 1000 ? ConsoleColor.Green :
            sensorData.PartsPerMillion < 2000 ? ConsoleColor.Yellow :
            sensorData.PartsPerMillion < 4000 ? ConsoleColor.DarkYellow :
            ConsoleColor.Red;
        Console.WriteLine($"{sensorData.PartsPerMillion}ppm");
        Console.ResetColor();
    }
}