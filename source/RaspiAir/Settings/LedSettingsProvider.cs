namespace RaspiAir.Settings;

using System.Threading.Tasks;
using DocumentStorage;
using LedStripe.Control.Settings;

internal class LedSettingsProvider(IDocumentStorage documentStorage) : ILedSettingsProvider
{
    private const string SettingsFileName = nameof(LedSettings);

    public async Task<LedSettings> ProvideAsync()
        => await documentStorage.ReadAsync<LedSettings>(SettingsFileName) ??
           await this.CreateAsync(SettingsFileName);

    private async Task<LedSettings> CreateAsync(string settingsFileName)
    {
        var defaultStoragesSettings = new LedSettings(8);
        await documentStorage.WriteAsync(defaultStoragesSettings, settingsFileName);
        return defaultStoragesSettings;
    }
}