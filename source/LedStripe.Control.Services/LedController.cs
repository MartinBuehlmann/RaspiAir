namespace LedStripe.Control.Services;

using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LedStripe.Control.LedBehaviors;
using LedStripe.Control.Services.LedBehaviorExecutors;
using LedStripe.Control.Settings;
using LedStripe.Device;

internal class LedController(
    ILedSettingsProvider ledSettingsProvider,
    LedBehaviorExecutorFactory ledBehaviorExecutorFactory,
    ILedStripeControlFactory ledStripeControlFactory)
    : ILedController, IDisposable
{
    private readonly Lock ledBehaviorExecutorsLock = new();
    private readonly SemaphoreSlim waitHandle = new(0);
    private ILedBehaviorExecutor[]? ledBehaviorExecutors;
    private Color[]? ledColors;

    public int LedCount
    {
        get
        {
            this.AssertInitialized();
            return this.ledBehaviorExecutors!.Length;
        }
    }

    public void SetLed(int ledIndex, ILedBehavior ledBehavior)
    {
        this.AssertInitialized();

        lock (this.ledBehaviorExecutorsLock)
        {
            this.ledBehaviorExecutors![ledIndex] = ledBehaviorExecutorFactory.Create(ledBehavior);
        }

        this.waitHandle.Release();
    }

    public async Task InitializeAsync()
    {
        LedSettings settings = await ledSettingsProvider.ProvideAsync();
        lock (this.ledBehaviorExecutorsLock)
        {
            this.ledBehaviorExecutors = this.GetInitialLedBehaviorExecutors(settings.LedCount);
        }

        this.waitHandle.Release();
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        this.AssertInitialized();

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await this.WaitForLedUpdateAsync(cancellationToken);

                Color[] newLedColors = this.GetNewLedColors();
                if (this.HasAnyLedColorChanged(newLedColors))
                {
                    this.UpdateLedColors(newLedColors);
                }
            }
            catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                // Ignore, system is shutting down.
            }
        }
    }

    public void Dispose()
    {
        this.AssertInitialized();

        using ILedStripeControl ledStripeControl = ledStripeControlFactory.Create(this.ledColors!.Length);
    }

    private void AssertInitialized()
    {
        if (this.ledBehaviorExecutors is null)
        {
            throw new InvalidOperationException("LedController is not initialized.");
        }
    }

    private ILedBehaviorExecutor[] GetInitialLedBehaviorExecutors(int ledCount)
        => Enumerable.Range(0, ledCount)
            .Select(ILedBehavior (_) => new OffLedBehavior())
            .Select(ledBehaviorExecutorFactory.Create)
            .ToArray();

    private async Task WaitForLedUpdateAsync(CancellationToken cancellationToken)
    {
        await Task.WhenAny(
            this.waitHandle.WaitAsync(cancellationToken),
            Task.Delay(50, cancellationToken));
    }

    private Color[] GetNewLedColors()
    {
        Color[] colors;
        lock (this.ledBehaviorExecutorsLock)
        {
            colors = this.ledBehaviorExecutors!.Select(x => x.GetColor()).ToArray();
        }

        return colors;
    }

    private bool HasAnyLedColorChanged(Color[] colors)
    {
        return !colors.SequenceEqual(this.ledColors ?? []);
    }

    private void UpdateLedColors(Color[] newLedColors)
    {
        this.ledColors = newLedColors;

        using ILedStripeControl ledStripeControl = ledStripeControlFactory.Create(this.ledColors.Length);
        for (int ledIndex = 0; ledIndex < this.ledColors.Length; ledIndex++)
        {
            ledStripeControl.SetPixelLed(ledIndex, this.ledColors[ledIndex]);
        }
    }
}