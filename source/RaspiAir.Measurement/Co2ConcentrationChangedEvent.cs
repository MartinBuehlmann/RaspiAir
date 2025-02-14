namespace RaspiAir.Measurement;

public record Co2ConcentrationChangedEvent(double Co2Concentration, DateTimeOffset Timestamp);