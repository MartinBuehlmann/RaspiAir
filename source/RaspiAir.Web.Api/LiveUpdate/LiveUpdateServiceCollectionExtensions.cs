namespace RaspiAir.Web.Api.LiveUpdate;

using System.Linq;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

public static class LiveUpdateServiceCollectionExtensions
{
    public static IServiceCollection AddSignalRServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBackgroundService, LiveUpdateBackgroundService>();

        serviceCollection.AddSignalR();
        serviceCollection.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/octet-stream"]);
        });
        return serviceCollection;
    }
}