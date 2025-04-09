namespace RaspiAir.Sensors.Demo;

using System;
using System.Security.Cryptography;
using System.Timers;
using AppServices.Common;

internal class DemoSensor : ISensor, IDisposable
{
    private Timer? timer;

    public event EventHandler<EventArgs<double>>? TemperatureChanged;

    public event EventHandler<EventArgs<double>>? HumidityChanged;

    public event EventHandler<EventArgs<int>>? Co2ConcentrationChanged;

    public void StartSensor()
    {
        this.timer = new Timer(TimeSpan.FromSeconds(10));
        this.timer.Elapsed += this.HandleTimerElapsed;
        this.timer.Start();
    }

    public void StopSensor()
    {
        this.timer!.Stop();
        this.timer!.Elapsed -= this.HandleTimerElapsed;
        this.timer.Dispose();
    }

    public void Dispose()
    {
        this.timer?.Dispose();
    }

    private static double GetRandomNumber(double minimum, double maximum)
    {
        byte[] randomBytes = RandomNumberGenerator.GetBytes(8);
        ulong x = BitConverter.ToUInt64(randomBytes, 0) / (1 << 11);
        double randomValue = x / (double)(1UL << 53);

        return (randomValue * (maximum - minimum)) + minimum;
    }

    private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        this.TemperatureChanged?.Invoke(this, new EventArgs<double>(GetRandomNumber(18.5, 21.2)));
        this.HumidityChanged?.Invoke(this, new EventArgs<double>(GetRandomNumber(30, 40)));
        this.Co2ConcentrationChanged?.Invoke(this, new EventArgs<int>((int)GetRandomNumber(300, 3000)));
    }
}