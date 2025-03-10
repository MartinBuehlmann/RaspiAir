namespace RaspiAir.Co2TrafficLight;

using Microsoft.Extensions.DependencyInjection;
using RaspiRobot.Lights.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCo2TrafficLightServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, Co2TrafficLightObserverService>();
        return serviceCollection;
    }
}