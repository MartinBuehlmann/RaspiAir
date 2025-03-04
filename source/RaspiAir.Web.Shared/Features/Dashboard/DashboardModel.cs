namespace RaspiAir.Web.Shared.Features.Dashboard;

using System;

public record DashboardModel(double Temperature, double Humidity, int Co2Concentration, DateTimeOffset Timestamp);