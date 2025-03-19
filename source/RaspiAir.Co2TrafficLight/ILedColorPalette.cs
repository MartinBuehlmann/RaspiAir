namespace RaspiAir.Co2TrafficLight;

using System.Drawing;

internal interface ILedColorPalette
{
    Color DarkGreen { get; }

    Color Green { get; }

    Color Yellow { get; }

    Color Red { get; }
}