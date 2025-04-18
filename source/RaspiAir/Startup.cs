﻿namespace RaspiAir;

using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using RaspiAir.Logging;
using RaspiAir.Web.Api.LiveUpdate;
using Serilog;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(o => { o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.CustomSchemaIds(type => type.ToString());
            c.SwaggerDoc("web", new OpenApiInfo { Title = "RaspiAir.WEB" });
            c.ResolveConflictingActions(x => x.First());
        });

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        services.AddSignalRServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseForwardedHeaders();
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseForwardedHeaders();
            app.UseHsts();
        }

        app.UseSerilogRequestLogging();

        app.UseFileServer(new FileServerOptions { StaticFileOptions = { ServeUnknownFileTypes = true } });

        app.UseSwagger(o =>
        {
            o.RouteTemplate = "swagger/{documentName}/swagger_v3.json";
            o.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
        });
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/web/swagger_v3.json", "RaspiAir.WEB");
            c.RoutePrefix = "swagger";
            c.DisplayRequestDuration();
        });

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseMiddleware<RequestLoggingMiddleware>();

        app.AddSignalRHubs();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}