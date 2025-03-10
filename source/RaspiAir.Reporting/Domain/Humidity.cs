namespace RaspiAir.Reporting.Domain;

using System;

public record Humidity(double Value, DateTimeOffset Timestamp)
{
    public ValueRating Rating
        => this.Value switch
        {
            <= 10 => ValueRating.Bad,
            <= 20 => ValueRating.NotSoGood,
            <= 30 => ValueRating.Good,
            <= 50 => ValueRating.Perfect,
            <= 60 => ValueRating.NotSoGood,
            <= 70 => ValueRating.Bad,
            _ => ValueRating.VeryBad,
        };
}