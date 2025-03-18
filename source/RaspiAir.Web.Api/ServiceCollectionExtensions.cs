namespace RaspiAir.Web.Api;

using Microsoft.Extensions.DependencyInjection;
using RaspiAir.Web.Api.Features.Dashboard;
using RaspiAir.Web.Api.LiveUpdate;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<LiveUpdateHub>();
        serviceCollection.AddTransient<ILiveUpdateEventObserver, MeasurementReportUpdatedObserver>();
        return serviceCollection;
    }
}