namespace RaspiAir.Web.Shared.Features.Dashboard;

public record Co2ConcentrationModel(double Value)
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