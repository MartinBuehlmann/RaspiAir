namespace AppServices.EventBroker;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

internal class EventBroker(IEventRegistration registration) : IEventBroker
{
    private int eventCount;

    public int QueuedEvents => this.eventCount;

    public void Publish<T>(T data)
        where T : class
    {
        foreach (IEventSubscriptionBase subscription in ((EventRegistration)registration).Retrieve(data))
        {
            this.FireAndForgetEvent(data, subscription);
        }
    }

    [SuppressMessage("Microsoft.VisualStudio.Threading.Analyzers", "VSTHRD110", Justification = "It's fire and forget")]
    private void FireAndForgetEvent<T>(T data, IEventSubscriptionBase subscription)
    {
        Interlocked.Increment(ref this.eventCount);
        Task.Run(() => this.DispatchEvenAsync(data, subscription));
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This method is designed to catch all exceptions.")]
    private async Task DispatchEvenAsync<T>(T data, IEventSubscriptionBase subscription)
    {
        try
        {
            if (subscription is IEventSubscriptionAsync<T> asyncSubscription)
            {
                await asyncSubscription.HandleAsync(data);
            }
            else
            {
                ((IEventSubscription<T>)subscription).Handle(data);
            }
        }
        catch (Exception exception)
        {
            throw new EventBrokerException("Unable to dispatch event.", exception);
        }
        finally
        {
            Interlocked.Decrement(ref this.eventCount);
        }
    }
}