namespace RaspiAir;

using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaspiAir.BackgroundServices;
using RaspiAir.Common;
using RaspiAir.Measurement;
using RaspiAir.Sensors.Scd41;
using Serilog;

public static class Program
{
    public static void Main(string[] args)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string appSettingsFileName =
            string.IsNullOrEmpty(environment) ? "appsettings.json" : $"appsettings.{environment}.json";

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path: appSettingsFileName, optional: false, reloadOnChange: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        string contentDirectory = $"{DirectoryProvider.ResolveContentDirectory()}/wwwroot";
        Log.Information("Setting the web root directory to '{ContentDirectory}'", contentDirectory);

        return Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder => webBuilder
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(Startup).Assembly.GetName().Name)
                .UseStartup<Startup>()
                .UseWebRoot(contentDirectory))
            .ConfigureServices(
                (_, services)
                    => services
                        .AddRaspiAirCommon()
                        .AddMeasurementApplication()
#if DEBUG
                        .AddMeasurementScd41()
#else
                        .AddMeasurementScd41()
#endif
                        .AddHostedService<BackgroundServiceHost>());
    }
}