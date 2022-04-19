using Microsoft.AspNetCore.SignalR.Client;
using Xunit;
using Xunit.Abstractions;

namespace RedRiftGame.Tests.Integration;

public class IntegrationTests
{
    private readonly ITestOutputHelper _output;

    public IntegrationTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public async Task GameShouldRun()
    {
        var connection1 = await CreateHubConnection("Елесей");
        var connection2 = await CreateHubConnection("Саша");

        await connection1.InvokeAsync("CreateMatch", "Елесей");
        await Task.Delay(30000);
    }
    
    [Fact]
    public async Task GameShouldRunEvenAfterThirdPlayerAttemptsToJoin()
    {
        var connection1 = await CreateHubConnection("Елесей");
        var connection2 = await CreateHubConnection("Саша");
        var connection3 = await CreateHubConnection("Дженерик");

        await connection1.InvokeAsync("CreateMatch", "Елесей");
        await Task.Delay(30000);
    }

    private async Task<HubConnection> CreateHubConnection(string clientName)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:11311/GameLobby")
            .WithAutomaticReconnect()
            .Build();

        connection.On<Guid, string>("ProcessNewMatch", (matchId, hostName) =>
        {
            var id = matchId.ToString()[..4];
            
            _output.WriteLine($"{clientName} New match created: {id} by {hostName}");
            try
            {
                connection.InvokeAsync("JoinMatch", matchId, clientName).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                _output.WriteLine($"{clientName} Unable to join: {e.Message}");
            }
        });

        connection.On<Guid, string, string>("ProcessGameStarted", (matchId, hostName, guestName) =>
        {
            var id = matchId.ToString()[..4];
            
            _output.WriteLine($"{clientName} Game started: {id} by {hostName} and {guestName}");
        });

        connection.On<Guid, string, string, int, int, bool>("ProcessGameFinished",
            (matchId, hostName, guestName, hostHp, guestHp, isHostWinner)
                =>
            {
                var id = matchId.ToString()[..4];
                _output.WriteLine(
                    $"{clientName} Game finished: {id} by {hostName} and {guestName} " +
                    $"with {hostHp} and {guestHp} hp left. " +
                    $"{(isHostWinner ? "Host won" : "Guest won")}");
            });

        await connection.StartAsync();
        return connection;
    }
}
