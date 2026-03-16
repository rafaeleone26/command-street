public class Program
{
    static void Main()
    {
        Board board = new Board();
        List<Player> players = new List<Player>
            {
                new Player("Player 1", 1000),
                new Player("Player 2", 1000)
            };

        Game game = new Game(board, players);
        game.Run();
    }
}
