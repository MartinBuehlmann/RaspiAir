namespace RaspiAir.Web.Features.Dashboard;

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class MeasurementReportUpdatedNotificationHub : Hub
{
    public async Task NotifyAsync()
    {
        await this.Clients.All.SendAsync("MeasurementReportUpdated");
    }
}