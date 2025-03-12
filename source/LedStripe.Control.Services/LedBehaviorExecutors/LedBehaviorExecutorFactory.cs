namespace LedStripe.Control.Services.LedBehaviorExecutors;

using System;
using LedStripe.Control.LedBehaviors;
using LedStripe.Control.Services.LedBehaviorExecutors.BlingkingColorLed;
using LedStripe.Control.Services.LedBehaviorExecutors.FadingColorLed;
using LedStripe.Control.Services.LedBehaviorExecutors.OffLed;
using LedStripe.Control.Services.LedBehaviorExecutors.SolidColorLed;

internal class LedBehaviorExecutorFactory
{
    public ILedBehaviorExecutor Create(ILedBehavior ledBehavior)
        => ledBehavior switch
        {
            BlinkingColorLedBehavior behavior => new BlinkingColorLedBehaviorExecutor(behavior),
            OffLedBehavior behavior => new OffLedBehaviorExecutor(behavior),
            SolidColorLedBehavior behavior => new SolidColorLedBehaviorExecutor(behavior),
            FadingColorLedBehavior behavior => new FadingColorLedBehaviorExecutor(behavior),
            _ => throw new ArgumentOutOfRangeException(nameof(ledBehavior)),
        };
}