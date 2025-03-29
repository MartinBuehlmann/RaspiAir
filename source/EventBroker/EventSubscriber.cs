namespace EventBroker;

using System;
using Microsoft.Extensions.DependencyInjection;

public class EventSubscriber(IServiceProvider serviceProvider)
{
    public void Subscribe(IEventSubscriptionBase eventSubscription)
    {
        var subscription = serviceProvider.GetService<IEventRegistration>()!;
        subscription.Register(eventSubscription);
    }

    public void Unsubscribe(IEventSubscriptionBase eventSubscription)
    {
        var subscription = serviceProvider.GetService<IEventRegistration>()!;
        subscription.Unregister(eventSubscription);
    }
}