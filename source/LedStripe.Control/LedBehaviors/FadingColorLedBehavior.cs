namespace LedStripe.Control.LedBehaviors;

using System.Drawing;

public class FadingColorLedBehavior(Color color) : ILedBehavior
{
    public Color Color => color;
}