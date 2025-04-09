namespace AppServices.LedStripe.Device.Demo;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLedStripeDemo(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ILedStripeControlFactory, LedStripeControlFactory>();
        return serviceCollection;
    }
}