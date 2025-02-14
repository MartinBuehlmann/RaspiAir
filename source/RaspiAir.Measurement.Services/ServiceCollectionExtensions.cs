namespace RaspiAir.Measurement.Services;

using Microsoft.Extensions.DependencyInjection;
using RaspiRobot.Lights.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMeasurementServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, MeasurementService>();
        return serviceCollection;
    }
}