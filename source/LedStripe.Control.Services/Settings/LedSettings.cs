namespace LedStripe.Control.Services.Settings;

public class LedSettings
{
    public LedSettings(int ledCount)
    {
        this.LedCount = ledCount;
    }

    public int LedCount { get; }
}