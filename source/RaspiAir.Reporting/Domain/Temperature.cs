namespace RaspiAir.Reporting.Domain;

using System;

public record Temperature(double Value, DateTimeOffset Timestamp);