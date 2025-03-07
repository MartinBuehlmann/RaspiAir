﻿namespace RaspiAir.Web.SignalR;

using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using RaspiRobot.Lights.Common;

public static class SignalRServiceCollectionExtensions
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