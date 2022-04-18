using FluentAssertions;
using RedRiftGame.Domain;
using Xunit;

namespace RedRiftGame.Tests.Domain;

public class MatchTests
{
    [Fact]
    public void ShouldAllowCreateARoom()
    {
        // arrange
        var hostId = Guid.NewGuid().ToString();
        var hostName = "Владимир";
        
        // act
        var match = Match.Create(hostId, hostName);
        
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
    public void ShouldAllowJoinGuestAfterCreation()
    {
        // arrange
        var match = Match.Create(Guid.NewGuid().ToString(), "Владимир");

        // act
        var guestId = Guid.NewGuid().ToString();
        var guestName = "Маша";
        
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
    public void ShouldAllowToRunNextTurn()
    {
        // arrange
        var match = Match.Create(Guid.NewGuid().ToString(), "Владимир");
        match.Join(Player.Create(Guid.NewGuid().ToString(), "Маша"));

        // act
        match.NextTurn();

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
        var match = Match.Create(Guid.NewGuid().ToString(), "Владимир");
        match.Join(Player.Create(Guid.NewGuid().ToString(), "Маша"));

        // act
        while (!match.IsFinished) 
            match.NextTurn();

        // assert
        match.CurrentTurn.Should().BeGreaterOrEqualTo(5);
        match.MatchState.Should().Be(MatchState.Finished);
        match.IsFinished.Should().BeTrue();
        (match.Host.Alive && match.Guest!.Alive).Should().BeFalse();
        
        // todo fails on draw, but it shouldn't be possible, needs clarification
        (match.Host.Alive || match.Guest!.Alive).Should().BeTrue();
    }

    [Fact]
    public void ShouldNotAllowToInterruptRunningMatch()
    {
        // arrange
        var match = Match.Create(Guid.NewGuid().ToString(), "Владимир");
        match.Join(Player.Create(Guid.NewGuid().ToString(), "Маша"));
        match.NextTurn();
        match.NextTurn();

        // act && assert
        Assert.Throws<Exception>(() => match.Interrupt());
    }

    [Fact]
    public void ShouldAllowToInterruptCreatedMatch()
    {
        // arrange
        var match = Match.Create(Guid.NewGuid().ToString(), "Владимир");

        // act
        match.Interrupt();

        // assert
        match.CurrentTurn.Should().Be(0);
        match.MatchState.Should().Be(MatchState.Interrupted);
        match.IsFinished.Should().BeTrue();
        match.Host.Alive.Should().BeTrue();
    }
}
