using RedRiftGame;
using RedRiftGame.Application;
using RedRiftGame.Application.Services;
using RedRiftGame.Hubs;
using RedRiftGame.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services
    .AddTransient<IMatchReporter, MatchReporter>()
    .AddHostedService<GameLobbyRunnerHostedService>()
    .AddApplication()
    .AddPersistence(configuration)
    .AddSignalR();

var app = builder.Build();
app.MapHub<GameLobbyHub>("/GameLobby");
app.Run();
