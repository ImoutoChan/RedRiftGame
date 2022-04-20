namespace RedRiftGame.Application.Services.Runner;

public interface IMatchReporter
{
    Task ReportAsync(CancellationToken ct);
}