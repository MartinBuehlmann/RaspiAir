namespace AppServices.EventBroker;

using Microsoft.Extensions.DependencyInjection;

public static class EventBrokerServiceCollectionExtensions
{
    public static IServiceCollection AddEventBroker(this IServiceCollection services)
    {
        services.AddSingleton<IEventBroker, AppServices.EventBroker.EventBroker>();
        services.AddSingleton<IEventRegistration, EventRegistration>();
        services.AddTransient<EventSubscriber>();
        return services;
    }
}