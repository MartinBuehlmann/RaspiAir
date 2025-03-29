namespace Common;

using Common.BackgroundServices;
using Common.Logging;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<Log>();
        return serviceCollection;
    }

    public static IServiceCollection AddBackgroundServiceHost(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService<BackgroundServiceHost>();
        return serviceCollection;
    }
}