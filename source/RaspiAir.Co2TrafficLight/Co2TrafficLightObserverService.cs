namespace RaspiAir.Co2TrafficLight;

using System.Threading;
using System.Threading.Tasks;
using Common;
using EventBroker;
using RaspiAir.Reporting;
using RaspiAir.Reporting.Domain;
using RaspiAir.Reporting.Events;

internal class Co2TrafficLightObserverService :
    IBackgroundService,
    IEventSubscriptionAsync<MeasurementReportUpdatedEvent>
{
    private readonly EventSubscriber eventSubscriber;
    private readonly IReportingRepository repository;

    public Co2TrafficLightObserverService(
        EventSubscriber eventSubscriber,
        IReportingRepository repository)
    {
        this.eventSubscriber = eventSubscriber;
        this.repository = repository;
    }

    public int Order => int.MaxValue;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.eventSubscriber.Subscribe(this);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.eventSubscriber.Unsubscribe(this);
        return Task.CompletedTask;
    }

    // TODO: Update LEDs according value rating.
    public async Task HandleAsync(MeasurementReportUpdatedEvent data)
    {
        Co2Concentration co2Concentration = await this.repository.RetrieveLatestCo2ConcentrationAsync();
    }
}