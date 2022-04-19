using MediatR;
using Microsoft.Extensions.Logging;
using NodaTime;
using RedRiftGame.Application.Cqs;

namespace RedRiftGame.Application.Services.Runner;

internal class GameLobbyRunner : IGameLobbyRunner
{
    private readonly IClock _clock;
    private readonly ILogger<GameLobbyRunner> _logger;
    private readonly IGameLobby _gameLobby;
    private readonly IMediator _mediator;
    private readonly PeriodicTimer _periodicTimer;
    private readonly IFinishedMatchesPipe _finishedMatchesPipe;

    public GameLobbyRunner(IGameLobby gameLobby, IMediator mediator, IClock clock, ILogger<GameLobbyRunner> logger, IFinishedMatchesPipe finishedMatchesPipe)
    {
        _gameLobby = gameLobby;
        _mediator = mediator;
        _clock = clock;
        _logger = logger;
        _finishedMatchesPipe = finishedMatchesPipe;
        _periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
    }

    public async Task RunMatchesAsync(CancellationToken ct)
    {
        while (await _periodicTimer.WaitForNextTickAsync(ct))
        {
            _gameLobby.RunMatches(_clock.GetCurrentInstant());

            var finishedMatches = _gameLobby.RemoveFinishedMatches();

            foreach (var finishedMatch in finishedMatches)
                await _finishedMatchesPipe.FinishedMatches.Writer.WriteAsync(finishedMatch, ct);
        }
    }

    public async Task ReportMatchesAsync(CancellationToken ct)
    {
        while (await _finishedMatchesPipe.FinishedMatches.Reader.WaitToReadAsync(ct))
        {
            var match = await _finishedMatchesPipe.FinishedMatches.Reader.ReadAsync(ct);

            try
            {
                await _mediator.Send(new ReportMatch(match), ct);
                await _mediator.Send(new SaveMatch(match), ct);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to report and match");
            }
        }
    }
}
