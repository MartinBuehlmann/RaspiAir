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

internal class Co2TrafficLightObserverService(
    EventSubscriber eventSubscriber,
    IReportingRepository repository,
    ILedController ledController,
    ILedColorPalette ledColorPalette)
    :
        IBackgroundService,
        IEventSubscriptionAsync<MeasurementReportUpdatedEvent>
{
    public int Order => int.MaxValue;

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

    public async Task HandleAsync(MeasurementReportUpdatedEvent data)
    {
        Co2Concentration co2Concentration = await repository.RetrieveLatestCo2ConcentrationAsync();
        this.UpdateLeds(co2Concentration.Rating);
    }

    private void UpdateLeds(ValueRating rating)
    {
        ILedBehavior[] ledBehaviors = Enumerable.Range(0, ledController.LedCount)
            .Select(ILedBehavior (_) => new OffLedBehavior())
            .ToArray();

        switch (rating)
        {
            case ValueRating.Perfect:
                this.VisualizeVeryBad(ledBehaviors);
                this.VisualizeBad(ledBehaviors);
                this.VisualizeNotSoGood(ledBehaviors);
                this.VisualizeGood(ledBehaviors);
                this.VisualizePerfect(ledBehaviors);
                break;
            case ValueRating.Good:
                this.VisualizeVeryBad(ledBehaviors);
                this.VisualizeBad(ledBehaviors);
                this.VisualizeNotSoGood(ledBehaviors);
                this.VisualizeGood(ledBehaviors);
                break;
            case ValueRating.NotSoGood:
                this.VisualizeVeryBad(ledBehaviors);
                this.VisualizeBad(ledBehaviors);
                this.VisualizeNotSoGood(ledBehaviors);
                break;
            case ValueRating.Bad:
                this.VisualizeVeryBad(ledBehaviors);
                this.VisualizeBad(ledBehaviors);
                break;
            case ValueRating.VeryBad:
                this.VisualizeVeryBad(ledBehaviors, true);
                break;
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(rating),
                    rating,
                    "Invalid value rating.");
        }

        for (int index = 0; index < ledController.LedCount; index++)
        {
            ledController.SetLed(index, ledBehaviors[index]);
        }
    }

    private void VisualizePerfect(ILedBehavior[] ledBehaviors)
    {
        ledBehaviors[7] = new SolidColorLedBehavior(ledColorPalette.DarkGreen);
        ledBehaviors[6] = new SolidColorLedBehavior(ledColorPalette.DarkGreen);
    }

    private void VisualizeGood(ILedBehavior[] ledBehaviors)
    {
        ledBehaviors[5] = new SolidColorLedBehavior(ledColorPalette.Green);
        ledBehaviors[4] = new SolidColorLedBehavior(ledColorPalette.Green);
    }

    private void VisualizeNotSoGood(ILedBehavior[] ledBehaviors)
    {
        ledBehaviors[3] = new SolidColorLedBehavior(ledColorPalette.Yellow);
        ledBehaviors[2] = new SolidColorLedBehavior(ledColorPalette.Yellow);
    }

    private void VisualizeBad(ILedBehavior[] ledBehaviors)
    {
        ledBehaviors[1] = new SolidColorLedBehavior(ledColorPalette.Red);
    }

    private void VisualizeVeryBad(ILedBehavior[] ledBehaviors, bool blinking = false)
    {
        if (blinking)
        {
            ledBehaviors[0] =
                new BlinkingColorLedBehavior(TimeSpan.FromSeconds(1), ledColorPalette.Red, Color.Black);
        }
        else
        {
            ledBehaviors[0] = new SolidColorLedBehavior(ledColorPalette.Red);
        }
    }
}