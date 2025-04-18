﻿namespace RaspiAir.Web.Shared.Features.Dashboard;

using System.Text.Json.Serialization;
using RaspiAir.Reporting.Domain;

public class HumidityModel
{
    public double Value { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ValueRating Rating { get; init; }
}