using RedRiftGame.Application;
using RedRiftGame.Application.Services;
using RedRiftGame.DataAccess;
using RedRiftGame.Hubs;
using RedRiftGame.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddTransient<IMatchReporter, MatchReporter>()
    .AddHostedService<GameLobbyRunnerHostedService>()
    .AddApplication()
    .AddPersistence(configuration)
    .AddSignalR();

var app = builder.Build();
app.MapHub<GameLobbyHub>("/GameLobby");
app.Run();
