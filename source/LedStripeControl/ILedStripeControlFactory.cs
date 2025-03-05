namespace LedStripeControl;

public interface ILedStripeControlFactory
{
    ILedStripeControl Create(int ledCount);
}