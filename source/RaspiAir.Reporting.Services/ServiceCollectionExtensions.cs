namespace RaspiAir.Reporting.Services;

using AppServices.Common.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;

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