namespace RaspiAir.Reporting;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RaspiAir.Reporting.Domain;

public interface IReportingRepository
{
    public Task<Temperature> RetrieveLatestTemperatureAsync();

    public Task<Humidity> RetrieveLatestHumidityAsync();

    public Task<Co2Concentration> RetrieveLatestCo2ConcentrationAsync();

    public Task<IReadOnlyList<Temperature>> RetrieveTemperatureHistoryAsync(DateTimeOffset date);

    public Task<IReadOnlyList<Humidity>> RetrieveHumidityHistoryAsync(DateTimeOffset date);

    public Task<IReadOnlyList<Co2Concentration>> RetrieveCo2ConcentrationHistoryAsync(DateTimeOffset date);
}