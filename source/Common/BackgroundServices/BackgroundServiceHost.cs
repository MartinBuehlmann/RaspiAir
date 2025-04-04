namespace Common.BackgroundServices;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

internal class BackgroundServiceHost(IEnumerable<IBackgroundService> backgroundServices) : IHostedService
{
    private readonly IReadOnlyList<IBackgroundService> backgroundServices = backgroundServices.ToList();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var orderedBackgroundServices = this.backgroundServices
            .GroupBy(x => x.Order)
            .OrderBy(x => x.Key);

        foreach (IGrouping<int, IBackgroundService> serviceGroup in orderedBackgroundServices)
        {
            await Task.WhenAll(serviceGroup.Select(service => service.StartAsync(cancellationToken)));
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var orderedBackgroundServices = this.backgroundServices
            .GroupBy(x => x.Order)
            .OrderByDescending(x => x.Key);

        foreach (IGrouping<int, IBackgroundService> serviceGroup in orderedBackgroundServices)
        {
            await Task.WhenAll(serviceGroup.Select(service => service.StopAsync(cancellationToken)));
        }
    }
}