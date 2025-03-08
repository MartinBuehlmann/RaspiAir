using RaspiAir.Web.LiveUpdate;

namespace RaspiAir.Web;

using Microsoft.Extensions.DependencyInjection;
using RaspiAir.Web.Features.Dashboard;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<LiveUpdateHub>();
        serviceCollection.AddTransient<ILiveUpdateEventObserver, MeasurementReportUpdatedObserver>();
        return serviceCollection;
    }
}