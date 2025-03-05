namespace RaspiAir.Reporting.Services.Entities;

using System;

internal class HumidityMeasurementEntity(double humidity, DateTimeOffset timestamp)
{
    public HumidityMeasurementEntity()
        : this(0, DateTimeOffset.MinValue)
    {
    }

    public double Humidity { get; set; } = humidity;

    public DateTimeOffset Timestamp { get; set; } = timestamp;
}