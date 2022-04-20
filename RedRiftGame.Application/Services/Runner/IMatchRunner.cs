namespace RedRiftGame.Application.Services.Runner;

public interface IMatchRunner
{
    Task RunAsync(CancellationToken ct);
}
