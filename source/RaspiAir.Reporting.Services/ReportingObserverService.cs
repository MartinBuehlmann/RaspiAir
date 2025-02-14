namespace RaspiAir.Reporting.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventBroker;
using RaspiAir.Measurement;
using RaspiRobot.Lights.Common;

internal class ReportingObserverService :
    IBackgroundService,
    IEventSubscriptionAsync<TemperatureChangedEvent>,
    IEventSubscriptionAsync<HumidityChangedEvent>,
    IEventSubscriptionAsync<Co2ConcentrationChangedEvent>
{
    private readonly Lock locker = new();
    private readonly EventSubscriber eventSubscriber;

    public ReportingObserverService(EventSubscriber eventSubscriber)
    {
        this.eventSubscriber = eventSubscriber;
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

    public Task HandleAsync(TemperatureChangedEvent data)
    {
        this.WriteToConsole(() =>
            Console.WriteLine($"{data.Timestamp.LocalDateTime}: Temperature: {data.Temperature:N1}C"));
        return Task.CompletedTask;
    }

    public Task HandleAsync(HumidityChangedEvent data)
    {
        this.WriteToConsole(() => Console.WriteLine($"{data.Timestamp.LocalDateTime}: Humidity   : {data.Humidity:N1}%"));
        return Task.CompletedTask;
    }

    public Task HandleAsync(Co2ConcentrationChangedEvent data)
    {
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
        return Task.CompletedTask;
    }

    private void WriteToConsole(Action action)
    {
        lock (this.locker)
        {
            action();
        }
    }
}