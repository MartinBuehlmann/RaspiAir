namespace RaspiAir.Measurement;

using System;

public record TemperatureChangedEvent(double Temperature, DateTimeOffset Timestamp);