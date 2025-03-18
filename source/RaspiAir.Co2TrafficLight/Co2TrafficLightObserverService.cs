namespace RaspiAir.Co2TrafficLight;

using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using EventBroker;
using LedStripe.Control;
using LedStripe.Control.LedBehaviors;
using RaspiAir.Reporting;
using RaspiAir.Reporting.Domain;
using RaspiAir.Reporting.Events;

internal class Co2TrafficLightObserverService :
    IBackgroundService,
    IEventSubscriptionAsync<MeasurementReportUpdatedEvent>
{
    private readonly EventSubscriber eventSubscriber;
    private readonly IReportingRepository repository;
    private readonly ILedController ledController;

    public Co2TrafficLightObserverService(
        EventSubscriber eventSubscriber,
        IReportingRepository repository,
        ILedController ledController)
    {
        this.eventSubscriber = eventSubscriber;
        this.repository = repository;
        this.ledController = ledController;
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

    public async Task HandleAsync(MeasurementReportUpdatedEvent data)
    {
        Co2Concentration co2Concentration = await this.repository.RetrieveLatestCo2ConcentrationAsync();
        ILedBehavior[] ledBehaviors = Enumerable.Range(0, this.ledController.LedCount)
            .Select(_ => (ILedBehavior)new OffLedBehavior())
            .ToArray();

        switch (co2Concentration.Rating)
        {
            case ValueRating.Perfect:
                VisualizeVeryBad(ledBehaviors);
                VisualizeBad(ledBehaviors);
                VisualizeNotSoGood(ledBehaviors);
                VisualizeGood(ledBehaviors);
                VisualizePerfect(ledBehaviors);
                break;
            case ValueRating.Good:
                VisualizeVeryBad(ledBehaviors);
                VisualizeBad(ledBehaviors);
                VisualizeNotSoGood(ledBehaviors);
                VisualizeGood(ledBehaviors);
                break;
            case ValueRating.NotSoGood:
                VisualizeVeryBad(ledBehaviors);
                VisualizeBad(ledBehaviors);
                VisualizeNotSoGood(ledBehaviors);
                break;
            case ValueRating.Bad:
                VisualizeVeryBad(ledBehaviors);
                VisualizeBad(ledBehaviors);
                break;
            case ValueRating.VeryBad:
                VisualizeVeryBad(ledBehaviors, true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        for (int index = 0; index < this.ledController.LedCount; index++)
        {
            this.ledController.SetLed(index, ledBehaviors[index]);
        }
    }

    private static void VisualizePerfect(ILedBehavior[] ledBehaviors)
    {
        ledBehaviors[7] = new SolidColorLedBehavior(Color.DarkGreen);
        ledBehaviors[6] = new SolidColorLedBehavior(Color.DarkGreen);
    }

    private static void VisualizeGood(ILedBehavior[] ledBehaviors)
    {
        ledBehaviors[5] = new SolidColorLedBehavior(Color.Green);
        ledBehaviors[4] = new SolidColorLedBehavior(Color.Green);
    }

    private static void VisualizeNotSoGood(ILedBehavior[] ledBehaviors)
    {
        ledBehaviors[3] = new SolidColorLedBehavior(Color.Yellow);
        ledBehaviors[2] = new SolidColorLedBehavior(Color.Yellow);
    }

    private static void VisualizeBad(ILedBehavior[] ledBehaviors)
    {
        ledBehaviors[1] = new SolidColorLedBehavior(Color.Red);
    }

    private static void VisualizeVeryBad(ILedBehavior[] ledBehaviors, bool blinking = false)
    {
        if (blinking)
        {
            ledBehaviors[0] = new BlinkingColorLedBehavior(TimeSpan.FromSeconds(1), Color.Red, Color.DarkRed);
        }
        else
        {
            ledBehaviors[0] = new SolidColorLedBehavior(Color.Red);
        }
    }
}