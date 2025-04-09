namespace RaspiAir.Web.Api.Features.Dashboard;

using System.Threading.Tasks;
using AppServices.EventBroker;
using Microsoft.AspNetCore.SignalR;
using RaspiAir.Reporting.Events;
using RaspiAir.Web.Api.LiveUpdate;
using RaspiAir.Web.Common.Events;

internal class MeasurementReportUpdatedObserver(IHubContext<LiveUpdateHub> hub) :
    IEventSubscriptionAsync<MeasurementReportUpdatedEvent>,
    ILiveUpdateEventObserver
{
    public async Task HandleAsync(MeasurementReportUpdatedEvent data)
    {
        await hub.Clients.All.SendAsync(EventTopics.MeasurementReportUpdated);
    }
}