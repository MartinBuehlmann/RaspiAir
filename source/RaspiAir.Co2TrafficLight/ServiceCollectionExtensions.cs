namespace RaspiAir.Co2TrafficLight;

using Common;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCo2TrafficLightServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, Co2TrafficLightObserverService>();
        return serviceCollection;
    }
}