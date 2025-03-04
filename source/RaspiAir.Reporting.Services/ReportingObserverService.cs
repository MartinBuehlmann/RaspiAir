namespace RaspiAir.Reporting.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventBroker;
using RaspiAir.Measurement;
using RaspiAir.Measurement.Events;
using RaspiAir.Reporting.Services.Entities;
using RaspiRobot.Lights.Common;

internal class ReportingObserverService :
    IBackgroundService,
    IEventSubscriptionAsync<TemperatureChangedEvent>,
    IEventSubscriptionAsync<HumidityChangedEvent>,
    IEventSubscriptionAsync<Co2ConcentrationChangedEvent>
{
    private readonly Lock locker = new();
    private readonly EventSubscriber eventSubscriber;
    private readonly ReportingRepository repository;

    public ReportingObserverService(
        EventSubscriber eventSubscriber,
        ReportingRepository repository)
    {
        this.eventSubscriber = eventSubscriber;
        this.repository = repository;
    }

    public int Order => 5;

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

    public async Task HandleAsync(TemperatureChangedEvent data)
    {
        await this.repository.SaveAsync(new TemperatureMeasurementEntity(data.Temperature, data.Timestamp));
        this.WriteToConsole(() =>
            Console.WriteLine($"{data.Timestamp.LocalDateTime}: Temperature: {data.Temperature:N1}C"));
    }

    public async Task HandleAsync(HumidityChangedEvent data)
    {
        await this.repository.SaveAsync(new HumidityMeasurementEntity(data.Humidity, data.Timestamp));
        this.WriteToConsole(() => Console.WriteLine($"{data.Timestamp.LocalDateTime}: Humidity   : {data.Humidity:N1}%"));
    }

    public async Task HandleAsync(Co2ConcentrationChangedEvent data)
    {
        await this.repository.SaveAsync(new Co2ConcentrationMeasurementEntity(data.Co2Concentration, data.Timestamp));
        this.WriteToConsole(() =>
        {
            Console.Write($"{data.Timestamp.LocalDateTime}: CO2        : ");
            Console.ForegroundColor =
                data.Co2Concentration < 400 ? ConsoleColor.DarkGreen :
                data.Co2Concentration < 1000 ? ConsoleColor.Green :
                data.Co2Concentration < 2000 ? ConsoleColor.Yellow :
                data.Co2Concentration < 4000 ? ConsoleColor.DarkYellow :
                ConsoleColor.Red;
            Console.WriteLine($"{data.Co2Concentration}ppm");
            Console.ResetColor();
        });
    }

    private void WriteToConsole(Action action)
    {
        lock (this.locker)
        {
            action();
        }
    }
}