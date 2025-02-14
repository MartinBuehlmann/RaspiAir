namespace RaspiAir.Measurement;

public record HumidityChangedEvent(double Humidity, DateTimeOffset Timestamp);