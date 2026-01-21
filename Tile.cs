enum TileType
{
    Start,
    Suit,
    Property
}

enum SuitType
{
    Red,
    Blue,
    Green,
    Yellow
}

class Tile
{
    public int Index;
    public TileType Type;

    public int BaseValue;   // only used for Property
    public Player? Owner;   // null = unowned
    public SuitType? Suit; // only used if Type == Suit

    public Tile(int index, TileType type, int baseValue = 0, SuitType? suit = null)
    {
        Index = index;
        Type = type;
        BaseValue = baseValue;
        Suit = suit;
        Owner = null;
    }
}
