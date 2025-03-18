namespace RaspiAir.Reporting.Domain;

using System;

public record Co2Concentration(int Value, DateTimeOffset Timestamp)
{
    public ValueRating Rating
        => this.Value switch
        {
            <= 400 => ValueRating.Perfect,
            <= 1000 => ValueRating.Good,
            <= 1500 => ValueRating.NotSoGood,
            <= 2000 => ValueRating.Bad,
            _ => ValueRating.VeryBad,
        };
}