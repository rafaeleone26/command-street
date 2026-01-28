public class Game
{
    private Board board;
    private Dice dice;
    private Player player;

    private int totalSuits;
    private int startBonus;
    private int turn;

    public Game()
    {
        board = new Board();
        dice = new Dice();
        player = new Player("Player 1", 1000);

        totalSuits = 4;
        startBonus = 300;
        turn = 1;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine($"\nTurn {turn}");

            int roll = dice.Roll();

            Console.WriteLine($"{player.Name} rolled {roll}");

            MovePlayer(player, board, roll);

            Tile tile = board.Tiles[player.Position];

            Console.WriteLine($"{player.Name} landed on tile {tile.Index} ({tile.Type})");

            ResolveLanding(player, tile);

            Console.WriteLine($"Wallet: {player.Wallet} | Net worth: {player.NetWorth}");

            Console.WriteLine("Press ENTER for next turn (or type q to quit)");

            string input = Console.ReadLine();

            if (input == "q")
                break;

            turn++;
        }
    }
    public void MovePlayer(Player player, Board board, int roll)
    {
        for (int step = 0; step < roll; step++)
        {
            player.Position++;

            if (player.Position >= board.Tiles.Count)
            {
                player.Position = 0;
            }

            Tile tile = board.Tiles[player.Position];

            Console.WriteLine($"  Passed tile {tile.Index} ({tile.Type})");

            HandlePassTile(player, tile);
        }
    }

    private void HandlePassTile(Player player, Tile tile)
    {
        if (tile.Type == TileType.Suit && tile.Suit.HasValue)
        {
            if (player.Suits.Add(tile.Suit.Value))
            {
                Console.WriteLine($"    Collected suit: {tile.Suit.Value}");
            }
        }

        if (tile.Type == TileType.Start)
        {
            if (player.HasAllSuits(totalSuits))
            {
                player.Wallet += startBonus;
                player.ClearSuits();

                Console.WriteLine($"    Full suits! Bonus +{startBonus}");
            }
        }
    }

    private void ResolveLanding(Player player, Tile tile)
    {
        if (tile.Type == TileType.Property)
        {
            if (tile.Owner == null)
            {
                if (player.Wallet >= tile.BaseValue)
                {
                    player.Wallet -= tile.BaseValue;
                    tile.Owner = player;
                    player.Properties.Add(tile);

                    Console.WriteLine($"Bought property for {tile.BaseValue}");
                }
            }
        }
    }
}
