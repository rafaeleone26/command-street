public class Program
{
    static void Main()
    {
        Board board = new Board();
        Player player = new Player("Player 1", 1000);

        Game game = new Game(board, player);
        game.Run();
    }
}
