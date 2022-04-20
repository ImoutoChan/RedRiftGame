using RedRiftGame;
using RedRiftGame.Application;
using RedRiftGame.DataAccess;
using RedRiftGame.Hubs;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddApplication()
    .AddPersistence(configuration)
    .AddHost();

var app = builder.Build();
app.MapHub<GameLobbyHub>("/GameLobby");
app.Run();
