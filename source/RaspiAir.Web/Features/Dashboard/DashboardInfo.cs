namespace RaspiAir.Web.Features.Dashboard;

using System;

public record DashboardInfo(double Temperature, double Humidity, int Co2Concentration, DateTimeOffset Timestamp);