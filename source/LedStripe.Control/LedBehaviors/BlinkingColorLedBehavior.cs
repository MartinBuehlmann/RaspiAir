namespace LedStripe.Control.LedBehaviors;

using System;
using System.Drawing;

public class BlinkingColorLedBehavior(TimeSpan interval, Color color, Color alternateColor) : ILedBehavior
{
    public TimeSpan Interval => interval;

    public Color Color => color;

    public Color AlternateColor => alternateColor;
}