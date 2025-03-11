using Common;

namespace RaspiAir.Measurement.Services;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMeasurementServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, MeasurementService>();
        return serviceCollection;
    }
}