namespace LedStripe.Control.Services;

using Common;
using LedStripe.Control.Services.LedBehaviorExecutors;
using LedStripe.Control.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLedStripe(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, LedService>();
        serviceCollection.AddSingleton<ILedController, LedController>();
        serviceCollection.AddTransient<LedBehaviorExecutorFactory>();
        serviceCollection.AddTransient<LedSettingsLoader>();
        return serviceCollection;
    }
}