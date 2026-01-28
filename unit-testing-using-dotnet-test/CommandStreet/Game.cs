using System.Reflection.Metadata;

public class Game
{
    private Board board;
    private Dice dice;
    private Player player;

    private int totalSuits;
    private int startBonus;
    private int turn;
    private bool isGameOver;
    public bool IsGameOver => isGameOver;
    private bool isSparkleMoment;
    public bool IsSparkleMoment => isSparkleMoment;

    private int winNetWorth = 5000;

    public Game(Board board, Player player)
    {
        this.board = board;
        this.player = player;
        dice = new Dice();

        totalSuits = 4;
        startBonus = 300;
        turn = 1;

        isGameOver = false;
    }

    public void Run()
    {
        while (!IsGameOver)
        {
            Console.WriteLine($"\nTurn {turn}");

            int roll = dice.Roll();

            Console.WriteLine($"{player.Name} rolled {roll}");

            MovePlayer(player, board, roll);

            if (IsGameOver) break;

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

            if (IsGameOver) break;
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
            if (player.NetWorth >= winNetWorth)
            {
                CheckWinCondition();
            }
            else if (player.HasAllSuits(totalSuits))
            {
                player.Wallet += startBonus;
                player.ClearSuits();

                Console.WriteLine($"    Full suits! Bonus +{startBonus}");
                    if (player.NetWorth >= winNetWorth)
                    {
                        isSparkleMoment = true;
                        Console.WriteLine($"Everything starts to sparkle");
                    }
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
    private void CheckWinCondition()
    {
        if (player.NetWorth >= winNetWorth)
        {
            Console.WriteLine("\n🎉 YOU WIN! 🎉");
            isGameOver = true;
        }
    }
}
