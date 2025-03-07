namespace RaspiAir.Web.Shared.Features.Dashboard;

public record TemperatureModel(double Value)
{
    public ValueRating Rating
        => this.Value switch
        {
            <= 10 => ValueRating.Bad,
            <= 15 => ValueRating.NotSoGood,
            <= 18 => ValueRating.Good,
            <= 22 => ValueRating.Perfect,
            <= 24 => ValueRating.Good,
            <= 27 => ValueRating.NotSoGood,
            <= 30 => ValueRating.Bad,
            _ => ValueRating.VeryBad,
        };
}