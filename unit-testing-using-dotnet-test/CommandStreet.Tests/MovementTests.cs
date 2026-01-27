namespace CommandStreet.Tests;

using Xunit;

public class MovementTests
{
    [Fact]
    public void Player_Wraps_Around_Board()
    {
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 10;

        Program.MovePlayer(player, board, roll: 3, totalSuits: 4, startBonus: 300);

        Assert.Equal(1, player.Position);
    }
    
    [Fact]
    public void Player_Collects_Suit_When_Passing_Suit_Tile()
    {
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 9;

        Program.MovePlayer(player, board, roll: 2, totalSuits: 4, startBonus: 300);

        Assert.Single(player.Suits);
    }

    [Fact]
    public void Player_Collects_Suit_When_Landing_Suit_Tile()
    {
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 9;

        Program.MovePlayer(player, board, roll: 1, totalSuits: 4, startBonus: 300);

        Assert.Single(player.Suits);
    }

    [Fact]
        public void Player_Gets_Bonus_When_Passing_Start_With_All_Suits()
    {
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 10;

        player.Suits.Add(SuitType.Red);
        player.Suits.Add(SuitType.Blue);
        player.Suits.Add(SuitType.Green);
        player.Suits.Add(SuitType.Yellow);

        int startingWallet = player.Wallet;

        Program.MovePlayer(player, board, roll: 3, totalSuits: 4, startBonus: 300);

        Assert.Equal(startingWallet + 300, player.Wallet);
        Assert.Empty(player.Suits);
    }

    [Fact]
    public void Player_Gets_Bonus_When_Landing_Start_With_All_Suits()
    {
        var board = new Board();
        var player = new Player("Test", 1000);

        player.Position = 10;

        player.Suits.Add(SuitType.Red);
        player.Suits.Add(SuitType.Blue);
        player.Suits.Add(SuitType.Green);
        player.Suits.Add(SuitType.Yellow);

        int startingWallet = player.Wallet;

        Program.MovePlayer(player, board, roll: 2, totalSuits: 4, startBonus: 300);

        Assert.Equal(startingWallet + 300, player.Wallet);
        Assert.Empty(player.Suits);
    }
}
