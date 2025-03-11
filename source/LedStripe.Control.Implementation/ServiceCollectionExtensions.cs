namespace LedStripe.Control.Implementation;

using Common;
using LedStripe.Control.Implementation.LedBehaviorExecutors;
using LedStripe.Control.Implementation.Settings;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLedsStripe(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, LedService>();
        serviceCollection.AddSingleton<ILedController, LedController>();
        serviceCollection.AddTransient<LedBehaviorExecutorFactory>();
        serviceCollection.AddTransient<LedSettingsLoader>();
        return serviceCollection;
    }
}