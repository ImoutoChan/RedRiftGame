using NodaTime;

namespace RedRiftGame.Application.Services.Runner;

internal class MatchRunner : IMatchRunner
{
    private readonly IClock _clock;
    private readonly IGameLobby _gameLobby;
    private readonly IFinishedMatchesPipe _finishedMatchesPipe;
    private readonly PeriodicTimer _periodicTimer;

    public MatchRunner(
        IGameLobby gameLobby,
        IClock clock,
        IFinishedMatchesPipe finishedMatchesPipe)
    {
        _gameLobby = gameLobby;
        _clock = clock;
        _finishedMatchesPipe = finishedMatchesPipe;
        _periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
    }

    public async Task RunAsync(CancellationToken ct)
    {
        while (await _periodicTimer.WaitForNextTickAsync(ct))
        {
            _gameLobby.RunMatches(_clock.GetCurrentInstant());

            var finishedMatches = _gameLobby.RemoveFinishedMatches();

            foreach (var finishedMatch in finishedMatches)
                await _finishedMatchesPipe.FinishedMatches.Writer.WriteAsync(finishedMatch, ct);
        }
    }
}
