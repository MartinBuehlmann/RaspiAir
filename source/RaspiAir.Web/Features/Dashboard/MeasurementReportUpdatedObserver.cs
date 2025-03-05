namespace RaspiAir.Web.Features.Dashboard;

using System.Threading.Tasks;
using EventBroker;
using Microsoft.AspNetCore.SignalR;
using RaspiAir.Reporting.Events;
using RaspiAir.Web.Shared.Events;
using RaspiAir.Web.SignalR;

internal class MeasurementReportUpdatedObserver : IEventSubscriptionAsync<MeasurementReportUpdatedEvent>, ILiveUpdateEventObserver
{
    private readonly IHubContext<MeasurementReportUpdatedHub> hub;

    public MeasurementReportUpdatedObserver(IHubContext<MeasurementReportUpdatedHub> hub)
    {
        this.hub = hub;
    }

    public async Task HandleAsync(MeasurementReportUpdatedEvent data)
    {
        await this.hub.Clients.All.SendAsync(EventTopics.MeasurementReportUpdated);
    }
}