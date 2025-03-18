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

        double stepWidth = this.ledController.LedCount / 5.0;

        switch (co2Concentration.Rating)
        {
            case ValueRating.Perfect:
                VisualizeVeryBad(ledBehaviors, stepWidth);
                VisualizeBad(ledBehaviors, stepWidth);
                VisualizeNotSoGood(ledBehaviors, stepWidth);
                VisualizeGood(ledBehaviors, stepWidth);
                VisualizePerfect(ledBehaviors, stepWidth);
                break;
            case ValueRating.Good:
                VisualizeVeryBad(ledBehaviors, stepWidth);
                VisualizeBad(ledBehaviors, stepWidth);
                VisualizeNotSoGood(ledBehaviors, stepWidth);
                VisualizeGood(ledBehaviors, stepWidth);
                break;
            case ValueRating.NotSoGood:
                VisualizeVeryBad(ledBehaviors, stepWidth);
                VisualizeBad(ledBehaviors, stepWidth);
                VisualizeNotSoGood(ledBehaviors, stepWidth);
                break;
            case ValueRating.Bad:
                VisualizeVeryBad(ledBehaviors, stepWidth);
                VisualizeBad(ledBehaviors, stepWidth);
                break;
            case ValueRating.VeryBad:
                VisualizeVeryBad(ledBehaviors, stepWidth);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        for (int index = 0; index < this.ledController.LedCount; index++)
        {
            this.ledController.SetLed(index, ledBehaviors[index]);
        }
    }

    private static void VisualizePerfect(ILedBehavior[] ledBehaviors, double stepWidth)
    {
        Visualize(ledBehaviors, stepWidth, 4, Color.DarkGreen);
    }

    private static void VisualizeGood(ILedBehavior[] ledBehaviors, double stepWidth)
    {
        Visualize(ledBehaviors, stepWidth, 3, Color.Green);
    }

    private static void VisualizeNotSoGood(ILedBehavior[] ledBehaviors, double stepWidth)
    {
        Visualize(ledBehaviors, stepWidth, 2, Color.Yellow);
    }

    private static void VisualizeBad(ILedBehavior[] ledBehaviors, double stepWidth)
    {
        Visualize(ledBehaviors, stepWidth, 1, Color.Red);
    }

    private static void VisualizeVeryBad(ILedBehavior[] ledBehaviors, double stepWidth)
    {
        Visualize(ledBehaviors, stepWidth, 0, Color.DarkRed);
    }

    private static void Visualize(ILedBehavior[] ledBehaviors, double stepWidth, int multiplier, Color color)
    {
        int startIndex = multiplier * (int)Math.Round(stepWidth);
        int endIndex = startIndex + (int)Math.Round(stepWidth);
        for (int i = startIndex; i < endIndex; i++)
        {
            ledBehaviors[i] = new SolidColorLedBehavior(color);
        }
    }
}