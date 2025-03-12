namespace LedStripe.Control.Services.Settings;

using System.Threading.Tasks;
using DocumentStorage;

public class LedSettingsLoader
{
    private const string SettingsFileName = nameof(LedSettings);
    private readonly IDocumentStorage documentStorage;

    public LedSettingsLoader(IDocumentStorage documentStorage)
    {
        this.documentStorage = documentStorage;
    }

    public async Task<LedSettings> LoadSettingsAsync()
        => await this.documentStorage.ReadAsync<LedSettings>(SettingsFileName) ??
           await this.CreateAsync(SettingsFileName);

    private async Task<LedSettings> CreateAsync(string settingsFileName)
    {
        var defaultStoragesSettings = new LedSettings(8);
        await this.documentStorage.WriteAsync(defaultStoragesSettings, settingsFileName);
        return defaultStoragesSettings;
    }
}