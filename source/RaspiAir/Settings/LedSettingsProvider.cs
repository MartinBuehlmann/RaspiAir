namespace RaspiAir.Settings;

using System.Threading.Tasks;
using LedStripe.Control.Settings;

internal class LedSettingsProvider : ILedSettingsProvider
{
    public Task<LedSettings> ProvideAsync()
        => Task.FromResult(new LedSettings(8));
}