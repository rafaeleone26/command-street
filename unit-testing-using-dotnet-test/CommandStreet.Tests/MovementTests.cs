namespace CommandStreet.Tests;

using Xunit;

public class MovementTests
{
    [Fact]
    public void Player_Wraps_Around_Board()
    {
        Board board = new Board();
        Player player = new Player("Test", 1000);

        player.Position = 10;
        player.Suits.Add(SuitType.Red);
        player.Suits.Add(SuitType.Blue);
        player.Suits.Add(SuitType.Green);
        player.Suits.Add(SuitType.Yellow);

        Program.MovePlayer(
            player: player,
            board: board,
            roll: 4,
            totalSuits: 4,
            startBonus: 300
        );

        Assert.Equal(2, player.Position);
        Assert.Equal(1, player.Suits.Count);
        Assert.Equal(1300, player.Wallet);
    }
}
