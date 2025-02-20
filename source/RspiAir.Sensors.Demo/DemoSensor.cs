using System.Timers;

namespace RspiAir.Sensors.Demo;

using System;
using RaspiAir.Sensors;

internal class DemoSensor : ISensor
{
    private Timer? timer;

    public event Action<double>? OnTemperatureChanged;

    public event Action<double>? OnHumidityChanged;

    public event Action<int>? OnCo2ConcentrationChanged;

    public void Start()
    {
        this.timer = new Timer(TimeSpan.FromSeconds(10));
        this.timer.Elapsed += this.HandleTimerElapsed;
        this.timer.Start();
    }

    public void Stop()
    {
        this.timer!.Stop();
        this.timer!.Elapsed -= this.HandleTimerElapsed;
        this.timer.Dispose();
    }

    private static double GetRandomNumber(double minimum, double maximum)
    {
        var random = new Random();
        return (random.NextDouble() * (maximum - minimum)) + minimum;
    }

    private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        this.OnTemperatureChanged?.Invoke(GetRandomNumber(18.5, 21.2));
        this.OnHumidityChanged?.Invoke(GetRandomNumber(30, 40));
        this.OnCo2ConcentrationChanged?.Invoke((int)GetRandomNumber(300, 3000));
    }
}