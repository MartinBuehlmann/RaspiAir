namespace RaspiAir.Sensors.Scd41;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMeasurementScd41(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISensor, Sensor>();
        return serviceCollection;
    }
}