namespace AppServices.EventBroker;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal class EventRegistration : IEventRegistration
{
    private readonly Lock subscriptionLock = new();
    private readonly Dictionary<Type, List<IEventSubscriptionBase>> subscriptions = new();

    public IReadOnlyList<IEventSubscriptionBase> Retrieve<TEventData>(TEventData data)
        where TEventData : class
    {
        lock (this.subscriptionLock)
        {
            Type eventDataType = data.GetType();
            if (this.subscriptions.TryGetValue(eventDataType, out var subscription))
            {
                return subscription.ToList();
            }
        }

        return [];
    }

    public void Register(IEventSubscriptionBase instance)
    {
        IReadOnlyList<Type> eventDataTypes = RetrieveEventDataTypes(instance);
        lock (this.subscriptionLock)
        {
            foreach (Type eventDataType in eventDataTypes)
            {
                if (!this.subscriptions.TryGetValue(eventDataType, out var value))
                {
                    value = new List<IEventSubscriptionBase>();
                    this.subscriptions.Add(eventDataType, value);
                }

                value.Add(instance);
            }
        }
    }

    public void Unregister(IEventSubscriptionBase instance)
    {
        IReadOnlyList<Type> eventDataTypes = RetrieveEventDataTypes(instance);
        lock (this.subscriptionLock)
        {
            foreach (Type eventDataType in eventDataTypes)
            {
                this.subscriptions[eventDataType].Remove(instance);
            }
        }
    }

    private static List<Type> RetrieveEventDataTypes(object instance)
    {
        List<Type> types = instance.GetType().GetInterfaces()
            .Where(x => x is { IsGenericType: true, GenericTypeArguments.Length: 1 })
            .Where(x => x.GetGenericTypeDefinition() == typeof(IEventSubscription<>) ||
                        x.GetGenericTypeDefinition() == typeof(IEventSubscriptionAsync<>))
            .SelectMany(x => x.GetGenericArguments())
            .ToList();

        if (types.Any())
        {
            return types;
        }

        throw new NotSupportedException(
            "Registered class must implement IEventSubscription or IEventSubscriptionAsync");
    }
}