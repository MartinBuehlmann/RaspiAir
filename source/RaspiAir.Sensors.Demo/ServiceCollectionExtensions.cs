namespace RaspiAir.Sensors.Demo;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDemoSensor(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISensor, DemoSensor>();
        return serviceCollection;
    }
}