namespace LedStripe.Device.Demo;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

internal class LedStripeControl(int ledCount) : ILedStripeControl
{
    private readonly Dictionary<int, Color> pixels = new();

    public void SetPixelLed(int x, Color color)
    {
        if (x < 0 || x >= ledCount)
        {
            throw new ArgumentOutOfRangeException(nameof(x), x, "X must be between 0 and " + ledCount);
        }

        this.pixels[x] = color;
    }

    public void Dispose()
    {
        foreach (KeyValuePair<int, Color> pixel in this.pixels.OrderBy(x => x.Key))
        {
            Console.ForegroundColor = EvaluateClosestConsoleColor(pixel.Value.R, pixel.Value.G, pixel.Value.B);
            Console.Write("\u2588 ");
        }

        Console.WriteLine();
    }

    internal LedStripeControl Initialize()
    {
        return this;
    }

    private static ConsoleColor EvaluateClosestConsoleColor(byte r, byte g, byte b)
    {
        ConsoleColor ret = 0;
        double rr = r, gg = g, bb = b, delta = double.MaxValue;

        foreach (ConsoleColor consoleColor in Enum.GetValues(typeof(ConsoleColor)))
        {
            string consoleColorName = Enum.GetName(typeof(ConsoleColor), consoleColor)!;
            Color color = System.Drawing.Color.FromName(consoleColorName == "DarkYellow" ? "Orange" : consoleColorName);
            double t = Math.Pow(color.R - rr, 2.0) + Math.Pow(color.G - gg, 2.0) + Math.Pow(color.B - bb, 2.0);
            if (t == 0.0)
            {
                return consoleColor;
            }

            if (t < delta)
            {
                delta = t;
                ret = consoleColor;
            }
        }

        return ret;
    }
}