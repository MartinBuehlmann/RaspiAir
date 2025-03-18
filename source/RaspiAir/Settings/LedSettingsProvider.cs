namespace RaspiAir.Settings;

using System.Threading.Tasks;
using DocumentStorage;
using LedStripe.Control.Settings;

internal class LedSettingsProvider : ILedSettingsProvider
{
    private const string SettingsFileName = nameof(LedSettings);
    private readonly IDocumentStorage documentStorage;

    public LedSettingsProvider(IDocumentStorage documentStorage)
    {
        this.documentStorage = documentStorage;
    }

    public async Task<LedSettings> ProvideAsync()
        => await this.documentStorage.ReadAsync<LedSettings>(SettingsFileName) ??
           await this.CreateAsync(SettingsFileName);

    private async Task<LedSettings> CreateAsync(string settingsFileName)
    {
        var defaultStoragesSettings = new LedSettings(8);
        await this.documentStorage.WriteAsync(defaultStoragesSettings, settingsFileName);
        return defaultStoragesSettings;
    }
}