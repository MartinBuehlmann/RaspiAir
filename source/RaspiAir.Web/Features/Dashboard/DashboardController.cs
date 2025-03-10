namespace RaspiAir.Web.Features.Dashboard;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaspiAir.Reporting;
using RaspiAir.Reporting.Domain;
using RaspiAir.Web.Shared.Features.Dashboard;

public class DashboardController : WebController
{
    private readonly IReportingRepository reportingRepository;

    public DashboardController(IReportingRepository reportingRepository)
    {
        this.reportingRepository = reportingRepository;
    }

    [HttpGet]
    public async Task<DashboardModel> RetrieveDashboardAsync()
    {
        Temperature temperature = await this.reportingRepository.RetrieveLatestTemperatureAsync();
        Humidity humidity = await this.reportingRepository.RetrieveLatestHumidityAsync();
        Co2Concentration co2Concentration = await this.reportingRepository.RetrieveLatestCo2ConcentrationAsync();
        return new DashboardModel(
            new TemperatureModel(temperature.Value, temperature.Rating),
            new HumidityModel(humidity.Value, humidity.Rating),
            new Co2ConcentrationModel(co2Concentration.Value, co2Concentration.Rating),
            new[] { temperature.Timestamp, humidity.Timestamp, co2Concentration.Timestamp }.Max());
    }
}