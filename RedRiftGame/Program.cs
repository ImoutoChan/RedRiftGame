using RedRiftGame;
using RedRiftGame.Application;
using RedRiftGame.Hubs;
using RedRiftGame.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services
    .AddHostedService<GameLobbyRunnerHostedService>()
    .AddApplication()
    .AddPersistence(configuration);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSignalR();


var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.MapHub<GameLobbyHub>("/GameLobby");
app.Run();
