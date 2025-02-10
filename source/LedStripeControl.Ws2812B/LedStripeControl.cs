namespace LedStripeControl.Ws2812B;

using System.Collections.Generic;
using System.Device.Spi;
using System.Drawing;
using Iot.Device.Ws28xx;

internal class LedStripeControl(int ledCount) : ILedStripeControl
{
    private readonly Dictionary<int, Color> pixels = new();
    private Ws2812b? device;

    public LedStripeControl Initialize()
    {
        var settings = new SpiConnectionSettings(0, 0)
        {
            ClockFrequency = 2_500_000,
            Mode = SpiMode.Mode0,
            DataBitLength = 8,
        };
        var spiDevice = SpiDevice.Create(settings);
        this.device = new Ws2812b(spiDevice, ledCount);
        return this;
    }

    public void SetPixelLed(int x, Color color)
    {
        this.pixels[x] = color;
    }

    public void Dispose()
    {
        RawPixelContainer image = this.device!.Image;
        image.Clear();

        foreach (KeyValuePair<int, Color> pixel in this.pixels)
        {
            image.SetPixel(pixel.Key, 0, pixel.Value);
        }

        this.device.Update();
    }
}