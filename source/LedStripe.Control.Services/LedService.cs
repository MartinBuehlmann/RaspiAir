﻿namespace LedStripe.Control.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using Common.BackgroundServices;

internal class LedService(ILedController ledController) : IBackgroundService
{
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);
    private readonly LedController ledController = (LedController)ledController;
    private readonly CancellationTokenSource cancellationTokenSource = new();
    private Task? ledServiceTask;

    public int Order => 10;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await this.ledController.InitializeAsync();
        this.ledServiceTask = Task.Factory.StartNew(
            async () => await this.ledController.RunAsync(this.cancellationTokenSource.Token),
            this.cancellationTokenSource.Token,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await this.cancellationTokenSource.CancelAsync();
        await this.ledServiceTask!.WaitAsync(Timeout, cancellationToken);
    }
}