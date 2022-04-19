using Microsoft.AspNetCore.SignalR.Client;
using Xunit;
using Xunit.Abstractions;

namespace RedRiftGame.Tests.Integration;

public class IntegrationTests
{
    private readonly ITestOutputHelper _output;

    public IntegrationTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public async Task ShouldPlayGame()
    {
        var connection1 = await CreateHubConnection(1);
        var connection2 = await CreateHubConnection(2);

        await connection1.SendAsync("CreateMatch", "Владимир");
        await Task.Delay(30000);
    }

    private async Task<HubConnection> CreateHubConnection(int number)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:11311/GameLobby")
            .WithAutomaticReconnect()
            .Build();

        connection.On<Guid, string>("ProcessNewMatch", (matchId, hostName) =>
        {
            _output.WriteLine($"{number} New match created: {matchId} by {hostName}");
            
            connection.SendAsync("JoinMatch", matchId, "Маша");
        });

        connection.On<Guid, string, string>("ProcessGameStarted", (matchId, hostName, guestName) =>
        {
            _output.WriteLine($"{number} Game started: {matchId} by {hostName} and {guestName}");
        });

        connection.On<Guid, string, string, int, int, bool>("ProcessGameFinished", 
            (matchId, hostName, guestName, hostHp, guestHp, isHostWinner)
                => _output.WriteLine($"{number} Game finished: {matchId} by {hostName} and {guestName} with {hostHp} and {guestHp} hp left. " + $"{(isHostWinner ? "Host won" : "Guest won")}"));

        await connection.StartAsync();
        return connection;
    }
}
