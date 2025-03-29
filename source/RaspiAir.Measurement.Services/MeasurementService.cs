namespace RaspiAir.Measurement.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using EventBroker;
using RaspiAir.Measurement.Events;
using RaspiAir.Sensors;

internal class MeasurementService(
    ISensor sensor,
    IEventBroker eventBroker) : IBackgroundService
{
    public int Order => 10;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        sensor.Start();
        sensor.OnTemperatureChanged += this.HandleTemperatureChanged;
        sensor.OnHumidityChanged += this.HandleHumidityChanged;
        sensor.OnCo2ConcentrationChanged += this.HandleCo2ConcentrationChanged;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        sensor.OnTemperatureChanged -= this.HandleTemperatureChanged;
        sensor.OnHumidityChanged -= this.HandleHumidityChanged;
        sensor.OnCo2ConcentrationChanged -= this.HandleCo2ConcentrationChanged;
        sensor.Stop();
        return Task.CompletedTask;
    }

    private void HandleTemperatureChanged(double value)
    {
        eventBroker.Publish(
            new TemperatureChangedEvent(value, DateTimeOffset.UtcNow));
    }

    private void HandleHumidityChanged(double value)
    {
        eventBroker.Publish(
            new HumidityChangedEvent(value, DateTimeOffset.UtcNow));
    }

    private void HandleCo2ConcentrationChanged(int value)
    {
        eventBroker.Publish(
            new Co2ConcentrationChangedEvent(value, DateTimeOffset.UtcNow));
    }
}