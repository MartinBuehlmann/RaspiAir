namespace RaspiAir.Reporting.Services;

using Microsoft.Extensions.DependencyInjection;
using RaspiRobot.Lights.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReportingServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, ReportingObserverService>();
        serviceCollection.AddTransient<ReportingRepository>();
        serviceCollection.AddTransient<IReportingRepository, ReportingRepository>();
        return serviceCollection;
    }
}