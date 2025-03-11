namespace LedStripe.Control.Implementation.LedBehaviorExecutors.SolidColorLed;

using System.Drawing;
using LedStripe.Control.LedBehaviors;

internal class SolidColorLedBehaviorExecutor(SolidColorLedBehavior behavior) : ILedBehaviorExecutor
{
    public Color GetColor()
        => behavior.Color;
}