namespace RaspiAir.Web.UI.Pages;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using RaspiAir.Reporting.Domain;
using RaspiAir.Web.Shared.Events;
using RaspiAir.Web.Shared.Features.Dashboard;

public partial class Home(NavigationManager navigation) : ComponentBase, IAsyncDisposable
{
    private readonly Dictionary<ValueRating, string> valueRatings = new()
    {
        { ValueRating.VeryBad, "#FF0000" },
        { ValueRating.Bad, "#ff9933" },
        { ValueRating.NotSoGood, "#ffcc00" },
        { ValueRating.Good, "#33cc33" },
        { ValueRating.Perfect, "#009933" },
    };

    private DashboardModel? model;
    private HubConnection? hubConnection;

    [Inject]
    private HttpClient HttpClient { get; set; } = null!;

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
            .WithUrl(navigation.ToAbsoluteUri($"/{EventTopics.MeasurementReportUpdatedHub}"))
            .WithAutomaticReconnect()
            .Build();

        this.hubConnection.Closed += (error) =>
        {
            Console.WriteLine("Connection closed");
            Console.WriteLine(error);
            return Task.CompletedTask;
        };

        this.hubConnection.On(
            EventTopics.MeasurementReportUpdated,
            async () => await this.OnMeasurementReportUpdatedAsync());

        await this.hubConnection.StartAsync();
    }

    private string GetBorderColor(ValueRating valueRating)
        => $"border-color: {this.valueRatings[valueRating]};";

    private Task OnMeasurementReportUpdatedAsync()
        => this.RefreshModelAsync();

    private async Task RefreshModelAsync()
    {
        this.model = await this.HttpClient.GetFromJsonAsync<DashboardModel>("web/Dashboard");
        await this.InvokeAsync(this.StateHasChanged);
    }
}