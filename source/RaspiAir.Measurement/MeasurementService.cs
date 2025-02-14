namespace RaspiAir.Measurement;

using System;
using System.Threading;
using System.Threading.Tasks;
using RaspiAir.Sensors;
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
        this.sensor.OnTemperatureChanged += this.HandleTemperatureChanged;
        this.sensor.OnHumidityChanged += this.HandleHumidityChanged;
        this.sensor.OnConcentrationChanged += this.HandleConcentrationChanged;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.sensor.OnTemperatureChanged -= this.HandleTemperatureChanged;
        this.sensor.OnHumidityChanged -= this.HandleHumidityChanged;
        this.sensor.OnConcentrationChanged -= this.HandleConcentrationChanged;
        this.sensor.Stop();
        return Task.CompletedTask;
    }

    private void HandleTemperatureChanged(double value)
    {
        Console.WriteLine($"Temperature: {value:N1}C");
    }

    private void HandleHumidityChanged(double value)
    {
        Console.WriteLine($"Humidity   : {value:N1}%");
    }

    private void HandleConcentrationChanged(double value)
    {
        Console.Write("CO2        : ");
        Console.ForegroundColor =
            value < 400 ? ConsoleColor.DarkGreen :
            value < 1000 ? ConsoleColor.Green :
            value < 2000 ? ConsoleColor.Yellow :
            value < 4000 ? ConsoleColor.DarkYellow :
            ConsoleColor.Red;
        Console.WriteLine($"{value}ppm");
        Console.ResetColor();
    }
}