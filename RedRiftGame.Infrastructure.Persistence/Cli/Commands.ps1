dotnet ef migrations add Init --project RedRiftGame.Infrastructure.Persistence --startup-project RedRiftGame --verbose
dotnet ef database update --project RedRiftGame.Infrastructure.Persistence --startup-project RedRiftGame --verbose
