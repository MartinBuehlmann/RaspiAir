namespace RaspiAir.Web.Api.LiveUpdate;

using Microsoft.AspNetCore.Builder;
using RaspiAir.Web.Common.Events;

public static class LiveUpdateApplicationBuilderExtensions
{
    public static IApplicationBuilder AddSignalRHubs(this IApplicationBuilder app)
    {
        app.UseResponseCompression();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<LiveUpdateHub>($"/{EventTopics.MeasurementReportUpdatedHub}");
        });

        return app;
    }
}