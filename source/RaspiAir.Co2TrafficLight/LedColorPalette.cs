namespace RaspiAir.Co2TrafficLight;

using System.Drawing;

internal class LedColorPalette : ILedColorPalette
{
    public Color DarkGreen => Color.FromArgb(0, 0, 5, 0);

    public Color Green => Color.FromArgb(0, 0, 10, 0);

    public Color Yellow => Color.FromArgb(0, 5, 5, 0);

    public Color Red => Color.FromArgb(0, 3, 0, 0);
}