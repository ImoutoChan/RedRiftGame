using RedRiftGame.Common;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Cqs;

public record CreateRoom(string ConnectionId, string Name) : ICommand;

public record JoinRoom(Guid RoomId, string ConnectionId, string Name) : ICommand;

public record InterruptMatch(string ConnectionId) : ICommand;

public record ReportMatch(Match Match) : ICommand;

public record SaveMatch(Match Match) : ICommand;
