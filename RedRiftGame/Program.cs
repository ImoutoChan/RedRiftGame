using RedRiftGame.Application;
using RedRiftGame.Application.Services;
using RedRiftGame.DataAccess;
using RedRiftGame.Hubs;
using RedRiftGame.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddTransient<IMatchClientReporter, SignalRMatchClientReporter>()
    .AddHostedService<MatchRunnerHostedService>()
    .AddHostedService<MatchReporterHostedService>()
    .AddApplication()
    .AddPersistence(configuration)
    .AddSignalR(x => x.EnableDetailedErrors = true);

var app = builder.Build();
app.MapHub<GameLobbyHub>("/GameLobby");
app.Run();
