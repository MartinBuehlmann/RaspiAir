namespace LedStripe.Device;

public interface ILedStripeControlFactory
{
    ILedStripeControl Create(int ledCount);
}