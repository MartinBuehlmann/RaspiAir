namespace RspiAir.Sensors.Demo;

using Microsoft.Extensions.DependencyInjection;
using RaspiAir.Sensors;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDemoSensor(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISensor, DemoSensor>();
        return serviceCollection;
    }
}