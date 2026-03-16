namespace CommandStreet.Tests;

using Xunit;

public class MultiplayerTurnTests
{
    [Fact]
    public void Game_Starts_With_First_Player()
    {
        var board = new Board();

        var players = new List<Player>
        {
            new Player("Player 1", 1000),
            new Player("Player 2", 1000)
        };

        var game = new Game(board, players);

        Assert.Equal(0, game.CurrentPlayerIndex);
    }

    [Fact]
    public void Turn_Switches_To_Next_Player()
    {
        var board = new Board();

        var players = new List<Player>
        {
            new Player("Player 1", 1000),
            new Player("Player 2", 1000)
        };

        var game = new Game(board, players);

        game.AdvanceTurn();

        Assert.Equal(1, game.CurrentPlayerIndex);
    }

    [Fact]
    public void Turn_Wraps_Back_To_First_Player()
    {
        var board = new Board();

        var players = new List<Player>
        {
            new Player("Player 1", 1000),
            new Player("Player 2", 1000)
        };

        var game = new Game(board, players);

        game.AdvanceTurn();
        game.AdvanceTurn();

        Assert.Equal(0, game.CurrentPlayerIndex);
    }
}