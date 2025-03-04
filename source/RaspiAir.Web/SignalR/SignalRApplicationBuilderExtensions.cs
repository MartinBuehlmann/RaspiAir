namespace RaspiAir.Web.SignalR;

using Microsoft.AspNetCore.Builder;
using RaspiAir.Web.Features.Dashboard;

public static class SignalRApplicationBuilderExtensions
{
    public static IApplicationBuilder AddSignalRHubs(this IApplicationBuilder app)
    {
        app.UseResponseCompression();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<MeasurementReportUpdatedNotificationHub>($"/{nameof(MeasurementReportUpdatedNotificationHub)}");
        });

        return app;
    }
}