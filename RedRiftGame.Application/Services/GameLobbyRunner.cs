using System.Threading.Channels;
using MediatR;
using NodaTime;
using RedRiftGame.Application.Cqs;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services;

// singleton
internal class GameLobbyRunner : IGameLobbyRunner
{
    private readonly IGameLobby _gameLobby;
    private readonly PeriodicTimer _periodicTimer;
    private readonly IMediator _mediator;
    private readonly IClock _clock;
    private readonly Channel<Match> _finishedMatches;

    public GameLobbyRunner(IGameLobby gameLobby, IMediator mediator, IClock clock)
    {
        _gameLobby = gameLobby;
        _mediator = mediator;
        _clock = clock;
        _periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        _finishedMatches = Channel.CreateUnbounded<Match>();
    }

    public async Task RunMatchesAsync(CancellationToken ct)
    {
        while (await _periodicTimer.WaitForNextTickAsync(ct))
        {
            var runningMatches = _gameLobby.CurrentMatches.Where(x => x.MatchState == MatchState.Running);

            foreach (var runningMatch in runningMatches)
            {
                runningMatch.NextTurn(_clock.GetCurrentInstant());
            }

            var finishedMatches = _gameLobby.CurrentMatches.Where(x => x.MatchState == MatchState.Finished).ToList();
            var finishedMatchesIds = finishedMatches.Select(x => x.Id).ToHashSet();

            _gameLobby.RemoveMatches(finishedMatchesIds);

            foreach (var finishedMatch in finishedMatches) 
                await _finishedMatches.Writer.WriteAsync(finishedMatch, ct);
        }
    }
    
    public async Task ReportMatchesAsync(CancellationToken ct)
    {
        while (await _finishedMatches.Reader.WaitToReadAsync(ct))
        {
            var match = await _finishedMatches.Reader.ReadAsync(ct);

            await _mediator.Send(new ReportMatch(match), ct);
            await _mediator.Send(new SaveMatch(match), ct);
        }
    }
}
