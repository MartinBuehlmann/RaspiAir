namespace LedStripe.Control.Implementation.LedBehaviorExecutors;

using System;
using LedStripe.Control.Implementation.LedBehaviorExecutors.BlingkingColorLed;
using LedStripe.Control.Implementation.LedBehaviorExecutors.FadingColorLed;
using LedStripe.Control.Implementation.LedBehaviorExecutors.OffLed;
using LedStripe.Control.Implementation.LedBehaviorExecutors.SolidColorLed;
using LedStripe.Control.LedBehaviors;

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