namespace RaspiAir.Reporting.Services.Entities;

using System;

internal class TemperatureMeasurementEntity(double temperature, DateTimeOffset timestamp)
{
    public TemperatureMeasurementEntity()
        : this(0, DateTimeOffset.MinValue)
    {
    }

    public double Temperature { get; set; } = temperature;

    public DateTimeOffset Timestamp { get; set; } = timestamp;
}