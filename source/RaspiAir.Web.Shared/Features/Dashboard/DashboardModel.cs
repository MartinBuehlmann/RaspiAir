namespace RaspiAir.Web.Shared.Features.Dashboard;

using System;

public record DashboardModel(TemperatureModel Temperature, HumidityModel Humidity, Co2ConcentrationModel Co2Concentration, DateTimeOffset Timestamp);