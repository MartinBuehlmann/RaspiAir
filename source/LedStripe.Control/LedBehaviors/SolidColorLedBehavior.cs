namespace LedStripe.Control.LedBehaviors;

using System.Drawing;

public record SolidColorLedBehavior(Color Color) : ILedBehavior;