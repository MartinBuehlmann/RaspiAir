namespace RaspiAir.Co2TrafficLight;

using Common.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCo2TrafficLightServices(this IServiceCollection serviceCollection)
    {
#if DEBUG
        serviceCollection.AddSingleton<ILedColorPalette, ConsoleLedColorPalette>();
#else
        serviceCollection.AddSingleton<ILedColorPalette, LedColorPalette>();
#endif
        serviceCollection.AddTransient<IBackgroundService, Co2TrafficLightObserverService>();
        return serviceCollection;
    }
}