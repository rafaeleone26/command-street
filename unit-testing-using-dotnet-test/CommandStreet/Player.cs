public class Player
{
    public string Name;
    public int Wallet;
    public int Position;
    public List<Tile> Properties = new List<Tile>();
    public HashSet<SuitType> Suits = new HashSet<SuitType>();

    public Player(string name, int startingMoney)
    {
        Name = name;
        Wallet = startingMoney;
        Position = 0;
    }

    public int NetWorth
    {
        get
        {
            int propertyValue = 0;
            foreach (var tile in Properties)
            {
                propertyValue += tile.BaseValue;
            }
            return Wallet + propertyValue;
        }
    }

    public bool HasAllSuits(int totalSuitCount)
    {
        return Suits.Count == totalSuitCount;
    }

    public void ClearSuits()
    {
        Suits.Clear();
    }
}
