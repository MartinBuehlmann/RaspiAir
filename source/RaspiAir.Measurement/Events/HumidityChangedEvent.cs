namespace RaspiAir.Measurement.Events;

using System;

public record HumidityChangedEvent(double Humidity, DateTimeOffset Timestamp);