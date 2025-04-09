namespace RaspiAir.Sensors;

using System;
using AppServices.Common;

public interface ISensor
{
    event EventHandler<EventArgs<double>> TemperatureChanged;

    event EventHandler<EventArgs<double>> HumidityChanged;

    event EventHandler<EventArgs<int>> Co2ConcentrationChanged;

    void StartSensor();

    void StopSensor();
}