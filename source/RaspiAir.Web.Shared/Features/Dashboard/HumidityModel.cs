namespace RaspiAir.Web.Shared.Features.Dashboard;

using RaspiAir.Reporting.Domain;

public record HumidityModel(double Value, ValueRating Rating);