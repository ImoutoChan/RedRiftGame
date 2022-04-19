using FluentAssertions;
using NodaTime;
using RedRiftGame.Domain;
using Xunit;

namespace RedRiftGame.Tests.Domain;

public class MatchTests
{
    [Fact]
    public void ShouldAllowToCreateARoom()
    {
        // arrange
        var now = SystemClock.Instance.GetCurrentInstant();
        var hostId = Guid.NewGuid().ToString();
        var hostName = "Елесей";

        // act
        var match = Match.Create(hostId, hostName, now);

        // assert
        match.Host.ConnectionId.Should().Be(hostId);
        match.Host.Name.Should().Be(hostName);
        match.Host.Health.Should().Be(10);
        match.Host.Alive.Should().BeTrue();

        match.MatchState.Should().Be(MatchState.Created);
        match.IsFinished.Should().BeFalse();
        match.Guest.Should().BeNull();
    }

    [Fact]
    public void ShouldAllowToJoinGuestAfterCreation()
    {
        // arrange
        var now = SystemClock.Instance.GetCurrentInstant();
        var match = Match.Create(Guid.NewGuid().ToString(), "Елесей", now);

        // act
        var guestId = Guid.NewGuid().ToString();
        var guestName = "Саша";

        match.Join(Player.Create(guestId, guestName));

        // assert
        match.Guest.Should().NotBeNull();
        match.Guest!.ConnectionId.Should().Be(guestId);
        match.Guest.Name.Should().Be(guestName);
        match.Guest.Health.Should().Be(10);
        match.Guest.Alive.Should().BeTrue();

        match.MatchState.Should().Be(MatchState.Running);
        match.IsFinished.Should().BeFalse();
    }

    [Fact]
    public void ShouldNotAllowToJoinGuestAfterFirstJoin()
    {
        // arrange
        var now = SystemClock.Instance.GetCurrentInstant();
        var match = Match.Create(Guid.NewGuid().ToString(), "Елесей", now);

        // act
        var guestId = Guid.NewGuid().ToString();
        var guestName = "Саша";

        match.Join(Player.Create(guestId, guestName));
        
        // assert
        Assert.Throws<MatchHandlingException>(() => match.Join(Player.Create(guestId, guestName)));
    }

    [Fact]
    public void ShouldAllowToRunNextTurn()
    {
        // arrange
        var now = SystemClock.Instance.GetCurrentInstant();
        var match = Match.Create(Guid.NewGuid().ToString(), "Елесей", now);
        match.Join(Player.Create(Guid.NewGuid().ToString(), "Саша"));

        // act
        match.NextTurn(now);

        // assert
        match.CurrentTurn.Should().Be(1);
        match.MatchState.Should().Be(MatchState.Running);
        match.IsFinished.Should().BeFalse();
        match.Host.Alive.Should().BeTrue();
        match.Guest!.Alive.Should().BeTrue();
    }

    [Fact]
    public void ShouldAllowToFinishMatch()
    {
        // arrange
        var now = SystemClock.Instance.GetCurrentInstant();
        var match = Match.Create(Guid.NewGuid().ToString(), "Елесей", now);
        match.Join(Player.Create(Guid.NewGuid().ToString(), "Саша"));

        // act
        while (!match.IsFinished)
            match.NextTurn(now);

        // assert
        match.CurrentTurn.Should().BeGreaterOrEqualTo(5);
        match.MatchState.Should().Be(MatchState.Finished);
        match.IsFinished.Should().BeTrue();
        (match.Host.Alive && match.Guest!.Alive).Should().BeFalse();

        (match.Host.Alive || match.Guest!.Alive).Should().BeTrue();
    }

    [Fact]
    public void ShouldNotAllowToInterruptRunningMatch()
    {
        // arrange
        var now = SystemClock.Instance.GetCurrentInstant();
        var match = Match.Create(Guid.NewGuid().ToString(), "Елесей", now);
        match.Join(Player.Create(Guid.NewGuid().ToString(), "Саша"));
        match.NextTurn(now);
        match.NextTurn(now);

        // act && assert
        Assert.Throws<MatchHandlingException>(() => match.Interrupt());
    }

    [Fact]
    public void ShouldAllowToInterruptCreatedMatch()
    {
        // arrange
        var now = SystemClock.Instance.GetCurrentInstant();
        var match = Match.Create(Guid.NewGuid().ToString(), "Елесей", now);

        // act
        match.Interrupt();

        // assert
        match.CurrentTurn.Should().Be(0);
        match.MatchState.Should().Be(MatchState.Interrupted);
        match.IsFinished.Should().BeTrue();
        match.Host.Alive.Should().BeTrue();
    }
}
