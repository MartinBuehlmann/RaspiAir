namespace RspiAir.Sensors.Demo;

using System;
using RaspiAir.Sensors;

// TODO: Implement simulation
internal class DemoSensor : ISensor
{
    public event Action<double>? OnTemperatureChanged;

    public event Action<double>? OnHumidityChanged;

    public event Action<int>? OnCo2ConcentrationChanged;

    public void Start()
    {
    }

    public void Stop()
    {
    }
}