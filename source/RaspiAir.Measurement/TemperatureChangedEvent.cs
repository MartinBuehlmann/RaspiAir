namespace RaspiAir.Measurement;

public record TemperatureChangedEvent(double Temperature, DateTimeOffset Timestamp);