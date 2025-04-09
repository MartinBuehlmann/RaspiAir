namespace AppServices.LedStripe.Device.Ws2812B;

public class LedStripeControlFactory : ILedStripeControlFactory
{
    public ILedStripeControl Create(int ledCount)
        => new LedStripeControl(ledCount)
            .Initialize();
}