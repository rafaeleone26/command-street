namespace CommandStreet.Tests;

using Xunit;

public class MovementTests
{
    [Fact]
    public void Player_Wraps_Around_Board()
    {
        Console.WriteLine("Test 1");
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 10;

        var game = new Game(board, player);
        game.MovePlayer(player, board, roll: 3);

        Assert.Equal(1, player.Position);
    }
    
    [Fact]
    public void Player_Collects_Suit_When_Passing_Suit_Tile()
    {
        Console.WriteLine("Test 2");
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 9;

        var game = new Game(board, player);
        game.MovePlayer(player, board, roll: 2);

        Assert.Single(player.Suits);
    }

    [Fact]
    public void Player_Collects_Suit_When_Landing_Suit_Tile()
    {
        Console.WriteLine("Test 3");
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 9;

        var game = new Game(board, player);
        game.MovePlayer(player, board, roll: 1);

        Assert.Single(player.Suits);
    }

    [Fact]
        public void Player_Gets_Bonus_When_Passing_Start_With_All_Suits()
    {
        Console.WriteLine("Test 4");
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 10;

        player.Suits.Add(SuitType.Red);
        player.Suits.Add(SuitType.Blue);
        player.Suits.Add(SuitType.Green);
        player.Suits.Add(SuitType.Yellow);

        int startingWallet = player.Wallet;

        var game = new Game(board, player);
        game.MovePlayer(player, board, roll: 3);

        Assert.Equal(startingWallet + 300, player.Wallet);
        Assert.Empty(player.Suits);
    }

    [Fact]
    public void Player_Gets_Bonus_When_Landing_Start_With_All_Suits()
    {
        Console.WriteLine("Test 5");
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 10;

        player.Suits.Add(SuitType.Red);
        player.Suits.Add(SuitType.Blue);
        player.Suits.Add(SuitType.Green);
        player.Suits.Add(SuitType.Yellow);

        int startingWallet = player.Wallet;

        var game = new Game(board, player);
        game.MovePlayer(player, board, roll: 2);

        Assert.Equal(startingWallet + 300, player.Wallet);
        Assert.Empty(player.Suits);
    }

    [Fact]
    public void Player_Wins_When_Passing_Start_With_Win_Condition()
    {
        Console.WriteLine("Test 6");
        var board = new Board();
        var player = new Player("Test", 5000);

        player.Position = 10;

        int startingWallet = player.Wallet;

        var game = new Game(board, player);
        game.MovePlayer(player, board, roll: 3);

        Assert.True(game.IsGameOver);
    }

    [Fact]
    public void Player_Wins_When_Landing_Start_With_Win_Condition()
    {
        Console.WriteLine("Test 7");
        var board = new Board();
        var player = new Player("Test", 5000);

        player.Position = 10;

        int startingWallet = player.Wallet;

        var game = new Game(board, player);
        game.MovePlayer(player, board, roll: 2);

        Assert.True(game.IsGameOver);
    }

    [Fact]
    public void Player_Achieves_Win_Condition_Passing_Start_Without_Win_Condition()
    {
        Console.WriteLine("Test 8");
        var board = new Board();
        var player = new Player("Test", 4700);

        player.Position = 10;

        player.Suits.Add(SuitType.Red);
        player.Suits.Add(SuitType.Blue);
        player.Suits.Add(SuitType.Green);
        player.Suits.Add(SuitType.Yellow);

        int startingWallet = player.Wallet;

        var game = new Game(board, player);
        game.MovePlayer(player, board, roll: 3);

        Assert.True(game.IsSparkleMoment);
        Assert.False(game.IsGameOver);
        Assert.Equal(5000, player.NetWorth);
    }

    [Fact]
    public void Player_Achieves_Win_Condition_Landing_Start_Without_Win_Condition()
    {
        Console.WriteLine("Test 9");
        var board = new Board();
        var player = new Player("Test", 4700);

        player.Position = 10;

        player.Suits.Add(SuitType.Red);
        player.Suits.Add(SuitType.Blue);
        player.Suits.Add(SuitType.Green);
        player.Suits.Add(SuitType.Yellow);

        int startingWallet = player.Wallet;

        var game = new Game(board, player);
        game.MovePlayer(player, board, roll: 2);

        Assert.True(game.IsSparkleMoment);
        Assert.False(game.IsGameOver);
        Assert.Equal(5000, player.NetWorth);
    }
}
