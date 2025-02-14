namespace RaspiAir.Sensors;

using System;

public interface ISensor
{
    event Action<double>? OnTemperatureChanged;

    event Action<double>? OnHumidityChanged;

    event Action<double>? OnConcentrationChanged;

    void Start();

    void Stop();
}