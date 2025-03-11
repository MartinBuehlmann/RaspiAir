namespace Common;

using Common.Logging;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<Log>();
        return serviceCollection;
    }
}