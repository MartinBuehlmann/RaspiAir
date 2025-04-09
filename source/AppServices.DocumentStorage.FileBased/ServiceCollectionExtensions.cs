namespace AppServices.DocumentStorage.FileBased;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileBasedDocumentStorage(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IDocumentStorage, FileStorage>();
        return serviceCollection;
    }
}