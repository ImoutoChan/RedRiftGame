using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services;

public interface IMatchReporter
{
    Task ReportAsync(Match match);
}