namespace RaspiAir.Sensors;

using System;

public interface ISensor
{
    event Action<double>? OnTemperatureChanged;

    event Action<double>? OnHumidityChanged;

    event Action<double>? OnCo2ConcentrationChanged;

    void Start();

    void Stop();
}