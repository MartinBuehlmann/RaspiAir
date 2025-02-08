namespace RaspiAir.Common;

using Microsoft.Extensions.DependencyInjection;
using RaspiAir.Common.Logging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRaspiAirCommon(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<Log>();
        return serviceCollection;
    }
}