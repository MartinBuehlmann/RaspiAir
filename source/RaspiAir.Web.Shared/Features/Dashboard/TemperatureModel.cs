namespace RaspiAir.Web.Shared.Features.Dashboard;

using RaspiAir.Reporting.Domain;

public record TemperatureModel(double Value, ValueRating Rating);