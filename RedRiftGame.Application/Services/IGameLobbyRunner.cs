namespace RedRiftGame.Application.Services;

public interface IGameLobbyRunner
{
    Task RunMatchesAsync(CancellationToken ct);

    Task ReportMatchesAsync(CancellationToken ct);
}
