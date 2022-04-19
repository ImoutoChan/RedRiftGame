using System.Threading.Channels;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services.Runner;

internal interface IFinishedMatchesPipe
{
    Channel<Match> FinishedMatches { get; }
}
