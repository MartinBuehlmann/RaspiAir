namespace RaspiAir.Web.Api.LiveUpdate;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using EventBroker;

internal class LiveUpdateBackgroundService(
    IEnumerable<ILiveUpdateEventObserver> liveUpdateEventObservers,
    EventSubscriber eventSubscriber)
    : IBackgroundService
{
    private readonly IReadOnlyList<ILiveUpdateEventObserver> liveUpdateEventObservers =
        liveUpdateEventObservers.ToList();

    public int Order => int.MaxValue;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (ILiveUpdateEventObserver liveUpdateEventObserver in this.liveUpdateEventObservers)
        {
            eventSubscriber.Subscribe(liveUpdateEventObserver);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (ILiveUpdateEventObserver liveUpdateEventObserver in this.liveUpdateEventObservers)
        {
            eventSubscriber.Unsubscribe(liveUpdateEventObserver);
        }

        return Task.CompletedTask;
    }
}