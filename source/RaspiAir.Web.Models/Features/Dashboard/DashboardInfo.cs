namespace RaspiAir.Web.Models.Features.Dashboard;

public record DashboardInfo(double Temperature, double Humidity, int Co2Concentration, DateTimeOffset Timestamp);