namespace RaspiAir.Web;

using Microsoft.Extensions.DependencyInjection;
using RaspiAir.Web.Features.Dashboard;
using RaspiAir.Web.SignalR;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<MeasurementReportUpdatedHub>();
        serviceCollection.AddTransient<ILiveUpdateEventObserver, MeasurementReportUpdatedObserver>();
        return serviceCollection;
    }
}