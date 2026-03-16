using System.Reflection.Metadata;

public class Game
{
    private Board board;
    private Dice dice;
    private List<Player> players;
    private int currentPlayerIndex;
    public int CurrentPlayerIndex => currentPlayerIndex;

    private int totalSuits;
    private int startBonus;
    private int turn;
    private bool isGameOver;
    public bool IsGameOver => isGameOver;
    private bool isSparkleMoment;
    public bool IsSparkleMoment => isSparkleMoment;

    private int winNetWorth = 5000;

    public Game(Board board, List<Player> players)
    {
        this.board = board;
        this.players = players;
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
            Player currentPlayer = players[currentPlayerIndex];

            Console.WriteLine($"\nTurn {turn}");

            int roll = dice.Roll();

            Console.WriteLine($"{currentPlayer.Name} rolled {roll}");

            MovePlayer(currentPlayer, board, roll);

            if (IsGameOver) break;

            Tile tile = board.Tiles[currentPlayer.Position];

            Console.WriteLine($"{currentPlayer.Name} landed on tile {tile.Index} ({tile.Type})");

            ResolveLanding(currentPlayer, tile);

            Console.WriteLine($"Wallet: {currentPlayer.Wallet} | Net worth: {currentPlayer.NetWorth}");

            Console.WriteLine("Press ENTER for next turn (or type q to quit)");

            string? input = Console.ReadLine();

            if (input == "q")
                break;

            AdvanceTurn();
        }
    }

    public void MovePlayer(Player currentPlayer, Board board, int roll)
    {
        for (int step = 0; step < roll; step++)
        {
            currentPlayer.Position++;

            if (currentPlayer.Position >= board.Tiles.Count)
            {
                currentPlayer.Position = 0;
            }

            Tile tile = board.Tiles[currentPlayer.Position];

            Console.WriteLine($"  Passed tile {tile.Index} ({tile.Type})");

            HandlePassTile(currentPlayer, tile);

            if (IsGameOver) break;
        }
    }

    private void HandlePassTile(Player currentPlayer, Tile tile)
    {
        if (tile.Type == TileType.Suit && tile.Suit.HasValue)
        {
            if (currentPlayer.Suits.Add(tile.Suit.Value))
            {
                Console.WriteLine($"    Collected suit: {tile.Suit.Value}");
            }
        }

        if (tile.Type == TileType.Start)
        {
            if (currentPlayer.NetWorth >= winNetWorth)
            {
                CheckWinCondition(currentPlayer);
            }
            else if (currentPlayer.HasAllSuits(totalSuits))
            {
                currentPlayer.Wallet += startBonus;
                currentPlayer.ClearSuits();

                Console.WriteLine($"    Full suits! Bonus +{startBonus}");
                    if (currentPlayer.NetWorth >= winNetWorth)
                    {
                        isSparkleMoment = true;
                        Console.WriteLine($"Everything starts to sparkle");
                    }
            }
        }
    }

    private void ResolveLanding(Player currentPlayer, Tile tile)
    {
        if (tile.Type == TileType.Property)
        {
            if (tile.Owner == null)
            {
                if (currentPlayer.Wallet >= tile.BaseValue)
                {
                    currentPlayer.Wallet -= tile.BaseValue;
                    tile.Owner = currentPlayer;
                    currentPlayer.Properties.Add(tile);

                    Console.WriteLine($"Bought property for {tile.BaseValue}");
                }
            }
        }
    }

    private void CheckWinCondition(Player currentPlayer)
    {
        if (currentPlayer.NetWorth >= winNetWorth)
        {
            Console.WriteLine("\n🎉 YOU WIN! 🎉");
            isGameOver = true;
        }
    }

    public void AdvanceTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        turn++;
    }
}
