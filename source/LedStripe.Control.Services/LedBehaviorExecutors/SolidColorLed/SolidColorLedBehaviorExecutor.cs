namespace LedStripe.Control.Services.LedBehaviorExecutors.SolidColorLed;

using System.Drawing;
using LedStripe.Control.LedBehaviors;

internal class SolidColorLedBehaviorExecutor(SolidColorLedBehavior behavior) : ILedBehaviorExecutor
{
    public Color GetColor()
        => behavior.Color;
}