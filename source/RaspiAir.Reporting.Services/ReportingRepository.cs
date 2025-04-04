namespace RaspiAir.Reporting.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentStorage;
using EventBroker;
using RaspiAir.Reporting.Domain;
using RaspiAir.Reporting.Events;
using RaspiAir.Reporting.Services.Entities;

internal class ReportingRepository(
    IDocumentStorage documentStorage,
    IEventBroker eventBroker)
    : IReportingRepository
{
    private const string LatestTemperatureFileName = "LatestTemperature";
    private const string LatestHumidityFileName = "LatestHumidity";
    private const string LatestCo2ConcentrationFileName = "LatestCo2Concentration";
    private const string TemperatureFilePrefix = "Temperature";
    private const string HumidityFilePrefix = "Humidity";
    private const string Co2ConcentrationFilePrefix = "Co2Concentration";

    public async Task SaveAsync(TemperatureMeasurementEntity entity)
    {
        await documentStorage.UpdateAsync<TemperatureMeasurementEntity>(
            LatestTemperatureFileName,
            x =>
            {
                x.Temperature = entity.Temperature;
                x.Timestamp = entity.Timestamp;
            });

        await documentStorage.UpdateAsync<MeasurementEntityCollection<TemperatureMeasurementEntity>>(
            DailyMeasurementFileNameBuilder.Build(TemperatureFilePrefix, entity.Timestamp),
            x => x.Items.Add(entity));

        eventBroker.Publish(new MeasurementReportUpdatedEvent());
    }

    public async Task SaveAsync(HumidityMeasurementEntity entity)
    {
        await documentStorage.UpdateAsync<HumidityMeasurementEntity>(
            LatestHumidityFileName,
            x =>
            {
                x.Humidity = entity.Humidity;
                x.Timestamp = entity.Timestamp;
            });

        await documentStorage.UpdateAsync<MeasurementEntityCollection<HumidityMeasurementEntity>>(
            DailyMeasurementFileNameBuilder.Build(HumidityFilePrefix, entity.Timestamp),
            x => x.Items.Add(entity));

        eventBroker.Publish(new MeasurementReportUpdatedEvent());
    }

    public async Task SaveAsync(Co2ConcentrationMeasurementEntity entity)
    {
        await documentStorage.UpdateAsync<Co2ConcentrationMeasurementEntity>(
            LatestCo2ConcentrationFileName,
            x =>
            {
                x.Co2Concentration = entity.Co2Concentration;
                x.Timestamp = entity.Timestamp;
            });

        await documentStorage.UpdateAsync<MeasurementEntityCollection<Co2ConcentrationMeasurementEntity>>(
            DailyMeasurementFileNameBuilder.Build(Co2ConcentrationFilePrefix, entity.Timestamp),
            x => x.Items.Add(entity));

        eventBroker.Publish(new MeasurementReportUpdatedEvent());
    }

    public async Task<Temperature> RetrieveLatestTemperatureAsync()
    {
        var entity = await documentStorage.ReadAsync<TemperatureMeasurementEntity>(LatestTemperatureFileName) ??
                     new TemperatureMeasurementEntity();
        return new Temperature(entity.Temperature, entity.Timestamp);
    }

    public async Task<Humidity> RetrieveLatestHumidityAsync()
    {
        var entity = await documentStorage.ReadAsync<HumidityMeasurementEntity>(LatestHumidityFileName) ??
                     new HumidityMeasurementEntity();
        return new Humidity(entity.Humidity, entity.Timestamp);
    }

    public async Task<Co2Concentration> RetrieveLatestCo2ConcentrationAsync()
    {
        var entity =
            await documentStorage.ReadAsync<Co2ConcentrationMeasurementEntity>(LatestCo2ConcentrationFileName) ??
            new Co2ConcentrationMeasurementEntity();
        return new Co2Concentration(entity.Co2Concentration, entity.Timestamp);
    }

    public async Task<IReadOnlyList<Temperature>> RetrieveTemperatureHistoryAsync(DateTimeOffset date)
    {
        MeasurementEntityCollection<TemperatureMeasurementEntity> values =
            await documentStorage.ReadAsync<MeasurementEntityCollection<TemperatureMeasurementEntity>>(
                DailyMeasurementFileNameBuilder.Build(TemperatureFilePrefix, date)) ??
            new MeasurementEntityCollection<TemperatureMeasurementEntity>();

        return values.Items
            .Select(x => new Temperature(x.Temperature, x.Timestamp))
            .ToList();
    }

    public async Task<IReadOnlyList<Humidity>> RetrieveHumidityHistoryAsync(DateTimeOffset date)
    {
        MeasurementEntityCollection<HumidityMeasurementEntity> values =
            await documentStorage.ReadAsync<MeasurementEntityCollection<HumidityMeasurementEntity>>(
                DailyMeasurementFileNameBuilder.Build(HumidityFilePrefix, date)) ??
            new MeasurementEntityCollection<HumidityMeasurementEntity>();

        return values.Items
            .Select(x => new Humidity(x.Humidity, x.Timestamp))
            .ToList();
    }

    public async Task<IReadOnlyList<Co2Concentration>> RetrieveCo2ConcentrationHistoryAsync(DateTimeOffset date)
    {
        MeasurementEntityCollection<Co2ConcentrationMeasurementEntity> values =
            await documentStorage.ReadAsync<MeasurementEntityCollection<Co2ConcentrationMeasurementEntity>>(
                DailyMeasurementFileNameBuilder.Build(Co2ConcentrationFilePrefix, date)) ??
            new MeasurementEntityCollection<Co2ConcentrationMeasurementEntity>();

        return values.Items
            .Select(x => new Co2Concentration(x.Co2Concentration, x.Timestamp))
            .ToList();
    }
}