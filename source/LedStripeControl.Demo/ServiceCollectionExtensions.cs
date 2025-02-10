namespace LedStripeControl.Demo;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLedStripeControlDemo(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ILedStripeControlFactory, LedStripeControlFactory>();
        return serviceCollection;
    }
}