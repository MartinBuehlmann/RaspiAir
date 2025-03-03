namespace RaspiAir.UI.Pages;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RaspiAir.Web.Models.Features.Dashboard;

public partial class Home : ComponentBase
{
    private DashboardInfo? model;

    [Inject]
    private HttpClient HttpClient { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        this.model = await this.HttpClient.GetFromJsonAsync<DashboardInfo>("web/Dashboard");
    }
}