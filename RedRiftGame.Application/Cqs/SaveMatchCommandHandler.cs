using MediatR;
using RedRiftGame.Application.Services;
using RedRiftGame.Common.Cqs;

namespace RedRiftGame.Application.Cqs;

internal class SaveMatchCommandHandler : ICommandHandler<SaveMatch>
{
    private readonly IMatchRepository _matchRepository;

    public SaveMatchCommandHandler(IMatchRepository matchRepository) => _matchRepository = matchRepository;

    public async Task<Unit> Handle(SaveMatch request, CancellationToken cancellationToken)
    {
        await _matchRepository.AppendAsync(request.Match);

        return Unit.Value;
    }
}