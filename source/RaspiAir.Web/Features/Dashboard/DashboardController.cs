namespace RaspiAir.Web.Features.Dashboard;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaspiAir.Reporting;
using RaspiAir.Reporting.Domain;
using RaspiAir.Web.Models.Features.Dashboard;

public class DashboardController : WebController
{
    private readonly IReportingRepository reportingRepository;

    public DashboardController(IReportingRepository reportingRepository)
    {
        this.reportingRepository = reportingRepository;
    }

    [HttpGet]
    public async Task<DashboardInfo> RetrieveDashboardAsync()
    {
        Temperature temperature = await this.reportingRepository.RetrieveLatestTemperatureAsync();
        Humidity humidity = await this.reportingRepository.RetrieveLatestHumidityAsync();
        Co2Concentration co2Concentration = await this.reportingRepository.RetrieveLatestCo2ConcentrationAsync();
        return new DashboardInfo(
            temperature.Value,
            humidity.Value,
            co2Concentration.Value,
            new[] { temperature.Timestamp, humidity.Timestamp, co2Concentration.Timestamp }.Max());
    }
}