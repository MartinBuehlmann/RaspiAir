namespace RaspiAir.Reporting.Domain;

using System;

public record Humidity(double Value, DateTimeOffset Timestamp);