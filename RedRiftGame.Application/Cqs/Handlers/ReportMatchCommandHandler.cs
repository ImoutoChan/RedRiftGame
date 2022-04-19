using MediatR;
using RedRiftGame.Application.Services;
using RedRiftGame.Common.Cqs;

namespace RedRiftGame.Application.Cqs.Handlers;

internal class ReportMatchCommandHandler : ICommandHandler<ReportMatch>
{
    private readonly IMatchReporter _matchReporter;

    public ReportMatchCommandHandler(IMatchReporter matchReporter)
    {
        _matchReporter = matchReporter;
    }

    public async Task<Unit> Handle(ReportMatch request, CancellationToken cancellationToken)
    {
        await _matchReporter.ReportAsync(request.Match);

        return Unit.Value;
    }
}