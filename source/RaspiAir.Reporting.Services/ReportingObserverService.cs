namespace RaspiAir.Reporting.Services;

using System.Threading;
using System.Threading.Tasks;
using AppServices.Common.BackgroundServices;
using AppServices.EventBroker;
using RaspiAir.Measurement.Events;
using RaspiAir.Reporting.Services.Entities;

internal class ReportingObserverService(
    EventSubscriber eventSubscriber,
    ReportingRepository repository)
    :
        IBackgroundService,
        IEventSubscriptionAsync<TemperatureChangedEvent>,
        IEventSubscriptionAsync<HumidityChangedEvent>,
        IEventSubscriptionAsync<Co2ConcentrationChangedEvent>
{
    public int Order => 5;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        eventSubscriber.Subscribe(this);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        eventSubscriber.Unsubscribe(this);
        return Task.CompletedTask;
    }

    public Task HandleAsync(TemperatureChangedEvent data)
        => repository.SaveAsync(new TemperatureMeasurementEntity(data.Temperature, data.Timestamp));

    public Task HandleAsync(HumidityChangedEvent data)
        => repository.SaveAsync(new HumidityMeasurementEntity(data.Humidity, data.Timestamp));

    public Task HandleAsync(Co2ConcentrationChangedEvent data)
        => repository.SaveAsync(new Co2ConcentrationMeasurementEntity(data.Co2Concentration, data.Timestamp));
}