namespace RaspiAir.Measurement;

using System;

public record HumidityChangedEvent(double Humidity, DateTimeOffset Timestamp);