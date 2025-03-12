namespace LedStripe.Control.Services.LedBehaviorExecutors.BlingkingColorLed;

using System;
using System.Drawing;
using LedStripe.Control.LedBehaviors;

public class BlinkingColorLedBehaviorExecutor(BlinkingColorLedBehavior behavior) : ILedBehaviorExecutor
{
    private DateTime lastBlinkTime = DateTime.Now - behavior.Interval;
    private Color currentColor = behavior.Color;

    public Color GetColor()
    {
        if (DateTime.Now - this.lastBlinkTime > behavior.Interval)
        {
            if (this.currentColor == behavior.Color)
            {
                this.currentColor = behavior.AlternateColor;
            }
            else
            {
                this.currentColor = behavior.Color;
            }

            this.lastBlinkTime = DateTime.Now;
        }

        return this.currentColor;
    }
}