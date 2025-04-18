﻿namespace RaspiAir.Measurement.Services;

using Common.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMeasurementServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, MeasurementService>();
        return serviceCollection;
    }
}