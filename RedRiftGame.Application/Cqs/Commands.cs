using RedRiftGame.Common.Cqs;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Cqs;

public record CreateMatch(string ConnectionId, string Name) : ICommand<Guid>;

public record JoinMatch(Guid RoomId, string ConnectionId, string Name) : ICommand<Match>;

public record InterruptMatch(string ConnectionId) : ICommand;

public record ReportMatch(Match Match) : ICommand;

public record SaveMatch(Match Match) : ICommand;
