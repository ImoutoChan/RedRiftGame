using MediatR;
using Microsoft.Extensions.Logging;
using RedRiftGame.Application.Cqs;

namespace RedRiftGame.Application.Services.Runner;

internal class MatchReporter : IMatchReporter
{
    private readonly ILogger<MatchReporter> _logger;
    private readonly IMediator _mediator;
    private readonly IFinishedMatchesPipe _finishedMatchesPipe;

    public MatchReporter(ILogger<MatchReporter> logger, IMediator mediator, IFinishedMatchesPipe finishedMatchesPipe)
    {
        _logger = logger;
        _mediator = mediator;
        _finishedMatchesPipe = finishedMatchesPipe;
    }

    public async Task ReportAsync(CancellationToken ct)
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
                _logger.LogError(e, "Failed to report and save");
            }
        }
    }
}