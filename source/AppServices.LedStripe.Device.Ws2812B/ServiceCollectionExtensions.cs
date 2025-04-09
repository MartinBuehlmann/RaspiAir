namespace AppServices.LedStripe.Device.Ws2812B;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLedStripeWs2812B(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ILedStripeControlFactory, LedStripeControlFactory>();
        return serviceCollection;
    }
}