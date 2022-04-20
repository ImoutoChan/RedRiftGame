using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services;

public interface IMatchClientReporter
{
    Task ReportAsync(Match match);
}
