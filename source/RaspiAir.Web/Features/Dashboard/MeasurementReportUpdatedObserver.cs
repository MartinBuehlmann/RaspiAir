namespace RaspiAir.Web.Features.Dashboard;

using System.Threading.Tasks;
using EventBroker;
using RaspiAir.Reporting.Events;
using RaspiAir.Web.SignalR;

internal class MeasurementReportUpdatedObserver : IEventSubscriptionAsync<MeasurementReportUpdatedEvent>, ILiveUpdateEventObserver
{
    private readonly MeasurementReportUpdatedHub hub;

    public MeasurementReportUpdatedObserver(MeasurementReportUpdatedHub hub)
    {
        this.hub = hub;
    }

    public async Task HandleAsync(MeasurementReportUpdatedEvent data)
    {
        await this.hub.NotifyAsync();
    }
}