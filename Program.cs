class Program
{
    static void Main()
    {
        Board board = new Board();
        Dice dice = new Dice();

        Player player = new Player("Player 1", 1000);

        int totalSuits = 4;
        int startBonus = 300;

        for (int turn = 1; turn <= 20; turn++)
        {
            Console.WriteLine($"\nTurn {turn}");

            int roll = dice.Roll();
            Console.WriteLine($"{player.Name} rolled {roll}");

            player.Position = (player.Position + roll) % board.Tiles.Count;
            Tile tile = board.Tiles[player.Position];

            Console.WriteLine($"{player.Name} landed on tile {tile.Index} ({tile.Type})");

            ResolveLanding(player, tile, totalSuits, startBonus);

            Console.WriteLine($"Wallet: {player.Wallet} | Net worth: {player.NetWorth}");
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

            case TileType.Suit:
                if (tile.Suit.HasValue)
                {
                    player.Suits.Add(tile.Suit.Value);
                    Console.WriteLine($"Collected suit: {tile.Suit.Value}");
                }
                break;

            case TileType.Start:
                if (player.HasAllSuits(totalSuits))
                {
                    player.Wallet += startBonus;
                    player.ClearSuits();
                    Console.WriteLine($"Completed all suits! Bonus +{startBonus}");
                }
                break;
        }
    }
}
