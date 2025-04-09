namespace RaspiAir.Web.Common.Features.Dashboard;

using System.Text.Json.Serialization;
using RaspiAir.Reporting.Domain;

public class TemperatureModel
{
    public double Value { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ValueRating Rating { get; init; }
}