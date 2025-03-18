namespace LedStripe.Control.Settings;

using System.Threading.Tasks;

public interface ILedSettingsProvider
{
    Task<LedSettings> ProvideAsync();
}