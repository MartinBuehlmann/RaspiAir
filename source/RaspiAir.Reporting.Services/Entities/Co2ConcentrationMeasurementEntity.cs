namespace RaspiAir.Reporting.Services.Entities;

using System;

internal class Co2ConcentrationMeasurementEntity(int co2Concentration, DateTimeOffset timestamp)
{
    public Co2ConcentrationMeasurementEntity()
        : this(0, DateTimeOffset.MinValue)
    {
    }

    public int Co2Concentration { get; set; } = co2Concentration;

    public DateTimeOffset Timestamp { get; set; } = timestamp;
}