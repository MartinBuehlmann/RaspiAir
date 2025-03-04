namespace RaspiAir.Web.Features.Dashboard;

using System.Threading.Tasks;
using EventBroker;
using RaspiAir.Reporting.Events;
using RaspiAir.Web.SignalR;

internal class MeasurementReportUpdatedObserver : IEventSubscriptionAsync<MeasurementReportUpdatedEvent>, ILiveUpdateEventObserver
{
    private readonly MeasurementReportUpdatedNotificationHub notificationHub;

    public MeasurementReportUpdatedObserver(MeasurementReportUpdatedNotificationHub notificationHub)
    {
        this.notificationHub = notificationHub;
    }

    public async Task HandleAsync(MeasurementReportUpdatedEvent data)
    {
        await this.notificationHub.NotifyAsync();
    }
}