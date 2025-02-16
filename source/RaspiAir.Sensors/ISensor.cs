namespace RaspiAir.Sensors;

using System;

public interface ISensor
{
    event Action<double>? OnTemperatureChanged;

    event Action<double>? OnHumidityChanged;

    event Action<int>? OnCo2ConcentrationChanged;

    void Start();

    void Stop();
}