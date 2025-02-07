namespace RaspiRobot.Lights.Common;

using System.Threading;
using System.Threading.Tasks;

public interface IBackgroundService
{
    int Order { get; }

    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);
}