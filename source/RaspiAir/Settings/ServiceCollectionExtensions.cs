namespace RaspiAir.Settings;

using LedStripe.Control.Settings;
using Microsoft.Extensions.DependencyInjection;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSettings(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ILedSettingsProvider, LedSettingsProvider>();
        return serviceCollection;
    }
}