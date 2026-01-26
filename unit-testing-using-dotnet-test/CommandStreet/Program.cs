public class Program
{
    static void Main()
    {
        Board board = new Board();
        Dice dice = new Dice();

        Player player = new Player("Player 1", 1000);

        int totalSuits = 4;
        int startBonus = 300;
        int turn = 1;

        while (true)
        {
            Console.WriteLine($"\nTurn {turn}");

            int roll = dice.Roll();

            Console.WriteLine($"{player.Name} rolled {roll}");

            MovePlayer(player, board, roll, totalSuits, startBonus);

            Tile tile = board.Tiles[player.Position];

            Console.WriteLine($"{player.Name} landed on tile {tile.Index} ({tile.Type})");

            ResolveLanding(player, tile, totalSuits, startBonus);

            Console.WriteLine($"Wallet: {player.Wallet} | Net worth: {player.NetWorth}");

            Console.WriteLine("Press ENTER for next turn (or type q to quit)");

            string input = Console.ReadLine();

            if (input == "q")
                break;

            turn++;
        }
    }

    public static void MovePlayer(
        Player player,
        Board board,
        int roll,
        int totalSuits,
        int startBonus)
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

            HandlePassTile(player, tile, totalSuits, startBonus);
        }
    }

    static void  HandlePassTile(
    Player player,
    Tile tile,
    int totalSuits,
    int startBonus)
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

    static void ResolveLanding(Player player, Tile tile, int totalSuits, int startBonus)
    {
        switch (tile.Type)
        {
            case TileType.Property:
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
                break;
        }
    }
}
