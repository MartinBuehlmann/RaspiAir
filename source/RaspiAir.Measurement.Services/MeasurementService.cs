namespace RaspiAir.Measurement.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using AppServices.Common;
using AppServices.Common.BackgroundServices;
using AppServices.EventBroker;
using RaspiAir.Measurement.Events;
using RaspiAir.Sensors;

internal class MeasurementService(
    ISensor sensor,
    IEventBroker eventBroker) : IBackgroundService
{
    public int Order => 10;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        sensor.StartSensor();
        sensor.TemperatureChanged += this.HandleTemperatureChanged;
        sensor.HumidityChanged += this.HandleHumidityChanged;
        sensor.Co2ConcentrationChanged += this.HandleCo2ConcentrationChanged;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        sensor.TemperatureChanged -= this.HandleTemperatureChanged;
        sensor.HumidityChanged -= this.HandleHumidityChanged;
        sensor.Co2ConcentrationChanged -= this.HandleCo2ConcentrationChanged;
        sensor.StopSensor();
        return Task.CompletedTask;
    }

    private void HandleTemperatureChanged(object? sender, EventArgs<double> e)
    {
        eventBroker.Publish(
            new TemperatureChangedEvent(e.Value, DateTimeOffset.UtcNow));
    }

    private void HandleHumidityChanged(object? sender, EventArgs<double> e)
    {
        eventBroker.Publish(
            new HumidityChangedEvent(e.Value, DateTimeOffset.UtcNow));
    }

    private void HandleCo2ConcentrationChanged(object? sender, EventArgs<int> e)
    {
        eventBroker.Publish(
            new Co2ConcentrationChangedEvent(e.Value, DateTimeOffset.UtcNow));
    }
}