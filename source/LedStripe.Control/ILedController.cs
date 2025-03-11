namespace LedStripe.Control;

public interface ILedController
{
    int LedCount { get; }

    void SetLed(int ledIndex, ILedBehavior ledBehavior);
}