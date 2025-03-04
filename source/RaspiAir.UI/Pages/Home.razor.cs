namespace RaspiAir.UI.Pages;

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RaspiAir.Web.Shared.Features.Dashboard;

public partial class Home : ComponentBase
{
    private DashboardModel? model;

    [Inject]
    private HttpClient HttpClient { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
#if DEBUG
        this.model = new DashboardModel(20.5234, 38.312, 450, DateTimeOffset.UtcNow);
#else
        this.model = await this.HttpClient.GetFromJsonAsync<DashboardInfo>("web/Dashboard");
#endif
    }
}