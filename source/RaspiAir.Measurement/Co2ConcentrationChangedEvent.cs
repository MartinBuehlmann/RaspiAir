namespace RaspiAir.Measurement;

using System;

public record Co2ConcentrationChangedEvent(int Co2Concentration, DateTimeOffset Timestamp);