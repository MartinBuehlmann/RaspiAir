namespace RaspiAir.Co2TrafficLight;

using System.Threading;
using System.Threading.Tasks;
using RaspiRobot.Lights.Common;

// TODO: Listen to MeasurementReportUpdatedEvent and update LEDs respectively
internal class Co2TrafficLightObserverService : IBackgroundService
{
    public int Order => int.MaxValue;

    public Task StartAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}