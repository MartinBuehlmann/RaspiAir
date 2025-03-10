namespace RaspiAir.Web.Shared.Features.Dashboard;

using RaspiAir.Reporting.Domain;

public record Co2ConcentrationModel(double Value, ValueRating Rating);