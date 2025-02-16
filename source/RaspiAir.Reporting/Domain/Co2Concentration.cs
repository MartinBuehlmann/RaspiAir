namespace RaspiAir.Reporting.Domain;

using System;

public record Co2Concentration(int Value, DateTimeOffset Timestamp);