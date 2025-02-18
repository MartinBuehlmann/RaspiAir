namespace RaspiAir.Sensors.Scd41;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScd41Sensor(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISensor, Scd41Sensor>();
        return serviceCollection;
    }
}