namespace RaspiAir.Measurement.Events;

using System;

public record Co2ConcentrationChangedEvent(int Co2Concentration, DateTimeOffset Timestamp);