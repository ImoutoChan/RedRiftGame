using System.Threading.Channels;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services.Runner;

// singleton
internal class FinishedMatchesPipe : IFinishedMatchesPipe
{
    public FinishedMatchesPipe()
    {
        FinishedMatches = Channel.CreateUnbounded<Match>();
    }

    public Channel<Match> FinishedMatches { get; }
}
