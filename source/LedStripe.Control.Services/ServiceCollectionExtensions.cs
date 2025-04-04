﻿namespace LedStripe.Control.Services;

using Common.BackgroundServices;
using LedStripe.Control.Services.LedBehaviorExecutors;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLedStripe(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, LedService>();
        serviceCollection.AddSingleton<ILedController, LedController>();
        serviceCollection.AddTransient<LedBehaviorExecutorFactory>();
        return serviceCollection;
    }
}