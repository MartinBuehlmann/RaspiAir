namespace RaspiAir.Web.LiveUpdate;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using EventBroker;

internal class LiveUpdateBackgroundService : IBackgroundService
{
    private readonly EventSubscriber eventSubscriber;
    private readonly IReadOnlyList<ILiveUpdateEventObserver> liveUpdateEventObservers;

    public LiveUpdateBackgroundService(
        IEnumerable<ILiveUpdateEventObserver> liveUpdateEventObservers,
        EventSubscriber eventSubscriber)
    {
        this.eventSubscriber = eventSubscriber;
        this.liveUpdateEventObservers = liveUpdateEventObservers.ToList();
    }

    public int Order => int.MaxValue;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (ILiveUpdateEventObserver liveUpdateEventObserver in this.liveUpdateEventObservers)
        {
            this.eventSubscriber.Subscribe(liveUpdateEventObserver);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (ILiveUpdateEventObserver liveUpdateEventObserver in this.liveUpdateEventObservers)
        {
            this.eventSubscriber.Unsubscribe(liveUpdateEventObserver);
        }

        return Task.CompletedTask;
    }
}