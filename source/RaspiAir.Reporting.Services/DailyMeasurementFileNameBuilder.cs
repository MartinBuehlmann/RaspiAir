namespace RaspiAir.Reporting.Services;

using System;

internal static class DailyMeasurementFileNameBuilder
{
    public static string Build(string entityName, DateTimeOffset timeStamp)
        => $"{entityName}_{timeStamp:yyyy-MM-dd}";
}