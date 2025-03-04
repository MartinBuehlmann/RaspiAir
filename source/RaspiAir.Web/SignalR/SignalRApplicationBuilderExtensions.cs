namespace RaspiAir.Web.SignalR;

using Microsoft.AspNetCore.Builder;
using RaspiAir.Web.Features.Dashboard;
using RaspiAir.Web.Shared.Events;

public static class SignalRApplicationBuilderExtensions
{
    public static IApplicationBuilder AddSignalRHubs(this IApplicationBuilder app)
    {
        app.UseResponseCompression();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<MeasurementReportUpdatedHub>($"/{EventTopics.MeasurementReportUpdatedHub}");
        });

        return app;
    }
}