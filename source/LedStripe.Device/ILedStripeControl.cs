namespace LedStripe.Device;

using System;
using System.Drawing;

public interface ILedStripeControl : IDisposable
{
    void SetPixelLed(int pixelIndex, Color color);
}