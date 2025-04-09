namespace AppServices.DocumentStorage.FileBased;

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class FileStorage : IDocumentStorage
{
    private const string RelativeDataDirectory = "../data";
    private readonly JsonSerializerSettings settings;
    private readonly string directory;
    private readonly ConcurrentDictionary<string, SemaphoreSlim> fileLocks;

    public FileStorage()
    {
        this.settings = new JsonSerializerSettings();
        this.fileLocks = new ConcurrentDictionary<string, SemaphoreSlim>();
        this.directory = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            RelativeDataDirectory);
        this.CreateDirectoryIfMissing();
    }

    public void RegisterConverter(params JsonConverter[] jsonConverters)
    {
        foreach (JsonConverter jsonConverter in jsonConverters)
        {
            this.settings.Converters.Add(jsonConverter);
        }
    }

    public async Task<T?> ReadAsync<T>(string file)
    {
        string filePath = this.GetFilePath(file);
        SemaphoreSlim semaphore = this.fileLocks.GetOrAdd(filePath, new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync();
        try
        {
            if (!File.Exists(filePath))
            {
                return default;
            }

            string jsonResult = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<T>(jsonResult, this.settings);
        }
        finally
        {
            semaphore.Release();
        }
    }

    public async Task WriteAsync<T>(T? data, string file)
    {
        string filePath = this.GetFilePath(file);
        SemaphoreSlim semaphore = this.fileLocks.GetOrAdd(filePath, new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync();
        try
        {
            string jsonResult = JsonConvert.SerializeObject(data, this.settings.Converters.ToArray());
            await File.WriteAllTextAsync(filePath, jsonResult);
        }
        finally
        {
            semaphore.Release();
        }
    }

    public async Task UpdateAsync<T>(string file, Action<T> updateAction)
        where T : new()
    {
        string filePath = Path.Combine(this.directory, $"{file}.json");
        SemaphoreSlim semaphore = this.fileLocks.GetOrAdd(filePath, new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync();

        try
        {
            T data = File.Exists(filePath)
                ? JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(filePath)) ?? new T()
                : new T();

            updateAction(data);
            string updatedContent = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(filePath, updatedContent);
        }
        finally
        {
            semaphore.Release();
        }
    }

    private string GetFilePath(string fileName)
        => Path.Combine(this.directory, $"{fileName}.json");

    private void CreateDirectoryIfMissing()
    {
        if (!Directory.Exists(this.directory))
        {
            Directory.CreateDirectory(this.directory);
        }
    }
}