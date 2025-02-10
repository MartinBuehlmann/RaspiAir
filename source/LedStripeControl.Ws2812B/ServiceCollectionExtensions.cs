namespace LedStripeControl.Ws2812B;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLedStripeControlWs2812B(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ILedStripeControlFactory, LedStripeControlFactory>();
        return serviceCollection;
    }
}