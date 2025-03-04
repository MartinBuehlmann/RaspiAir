namespace RaspiAir.Measurement.Events;

using System;

public record TemperatureChangedEvent(double Temperature, DateTimeOffset Timestamp);