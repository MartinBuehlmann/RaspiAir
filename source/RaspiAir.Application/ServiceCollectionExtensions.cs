namespace RaspiAir.Application;

using Microsoft.Extensions.DependencyInjection;
using RaspiRobot.Lights.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMeasurementApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, MeasurementService>();
        return serviceCollection;
    }
}