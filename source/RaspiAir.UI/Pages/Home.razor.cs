namespace RaspiAir.UI.Pages;

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using RaspiAir.Web.Shared.Events;
using RaspiAir.Web.Shared.Features.Dashboard;

public partial class Home : ComponentBase, IAsyncDisposable
{
    private readonly NavigationManager navigation;
    private DashboardModel? model;
    private HubConnection? hubConnection;

    public Home(NavigationManager navigation)
    {
        this.navigation = navigation;
    }

    [Inject] private HttpClient HttpClient { get; set; } = null!;

    public async ValueTask DisposeAsync()
    {
        if (this.hubConnection is not null)
        {
            await this.hubConnection.DisposeAsync();
        }
    }

    protected override async Task OnInitializedAsync()
    {
            await this.RefreshModelAsync();

            this.hubConnection = new HubConnectionBuilder()
                .WithUrl(this.navigation.ToAbsoluteUri($"/{EventTopics.MeasurementReportUpdatedHub}"))
                .Build();

            this.hubConnection.Closed += (error) =>
            {
                Console.WriteLine("Connection closed");
                Console.WriteLine(error);
                return Task.CompletedTask;
            };

            this.hubConnection.On(EventTopics.MeasurementReportUpdated, this.OnMeasurementReportUpdatedAsync);

            await this.hubConnection.StartAsync();
    }

    private async Task OnMeasurementReportUpdatedAsync()
    {
        Console.WriteLine("Measurement report updated");
        await this.RefreshModelAsync();
    }

    private async Task RefreshModelAsync()
    {
        this.model = await this.HttpClient.GetFromJsonAsync<DashboardModel>("web/Dashboard");
    }
}