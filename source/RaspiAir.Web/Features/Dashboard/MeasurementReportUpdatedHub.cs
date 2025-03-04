namespace RaspiAir.Web.Features.Dashboard;

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RaspiAir.Web.Shared.Events;

public class MeasurementReportUpdatedHub : Hub
{
    public async Task NotifyAsync()
    {
        await this.Clients.All.SendAsync(EventTopics.MeasurementReportUpdated);
    }
}