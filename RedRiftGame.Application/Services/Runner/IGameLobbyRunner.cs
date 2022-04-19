namespace RedRiftGame.Application.Services.Runner;

public interface IGameLobbyRunner
{
    Task RunMatchesAsync(CancellationToken ct);

    Task ReportMatchesAsync(CancellationToken ct);
}
