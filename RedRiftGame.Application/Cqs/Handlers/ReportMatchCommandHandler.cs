using MediatR;
using RedRiftGame.Application.Services;
using RedRiftGame.Common.Cqs;

namespace RedRiftGame.Application.Cqs.Handlers;

internal class ReportMatchCommandHandler : ICommandHandler<ReportMatch>
{
    private readonly IMatchClientReporter _matchClientReporter;

    public ReportMatchCommandHandler(IMatchClientReporter matchClientReporter)
    {
        _matchClientReporter = matchClientReporter;
    }

    public async Task<Unit> Handle(ReportMatch request, CancellationToken cancellationToken)
    {
        await _matchClientReporter.ReportAsync(request.Match);

        return Unit.Value;
    }
}