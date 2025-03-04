namespace RaspiAir.Measurement.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventBroker;
using RaspiAir.Measurement.Events;
using RaspiAir.Sensors;
using RaspiRobot.Lights.Common;

internal class MeasurementService : IBackgroundService
{
    private readonly ISensor sensor;
    private readonly IEventBroker eventBroker;

    public MeasurementService(
        ISensor sensor,
        IEventBroker eventBroker)
    {
        this.sensor = sensor;
        this.eventBroker = eventBroker;
    }

    public int Order => 10;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.sensor.Start();
        this.sensor.OnTemperatureChanged += this.HandleTemperatureChanged;
        this.sensor.OnHumidityChanged += this.HandleHumidityChanged;
        this.sensor.OnCo2ConcentrationChanged += this.HandleCo2ConcentrationChanged;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.sensor.OnTemperatureChanged -= this.HandleTemperatureChanged;
        this.sensor.OnHumidityChanged -= this.HandleHumidityChanged;
        this.sensor.OnCo2ConcentrationChanged -= this.HandleCo2ConcentrationChanged;
        this.sensor.Stop();
        return Task.CompletedTask;
    }

    private void HandleTemperatureChanged(double value)
    {
        this.eventBroker.Publish(
            new TemperatureChangedEvent(value, DateTimeOffset.UtcNow));
    }

    private void HandleHumidityChanged(double value)
    {
        this.eventBroker.Publish(
            new HumidityChangedEvent(value, DateTimeOffset.UtcNow));
    }

    private void HandleCo2ConcentrationChanged(int value)
    {
        this.eventBroker.Publish(
            new Co2ConcentrationChangedEvent(value, DateTimeOffset.UtcNow));
    }
}