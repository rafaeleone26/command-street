class Board
{
    public List<Tile> Tiles = new List<Tile>();

    public Board()
    {
        Tiles.Add(new Tile(0, TileType.Start));

        Tiles.Add(new Tile(1, TileType.Property, 100));
        Tiles.Add(new Tile(2, TileType.Suit, suit: SuitType.Red));
        Tiles.Add(new Tile(3, TileType.Property, 120));
        Tiles.Add(new Tile(4, TileType.Suit, suit: SuitType.Blue));

        Tiles.Add(new Tile(5, TileType.Property, 140));
        Tiles.Add(new Tile(6, TileType.Property, 160));
        Tiles.Add(new Tile(7, TileType.Property, 180));
        Tiles.Add(new Tile(8, TileType.Suit, suit: SuitType.Green));
        Tiles.Add(new Tile(9, TileType.Property, 200));

        Tiles.Add(new Tile(10, TileType.Suit, suit: SuitType.Yellow));
        Tiles.Add(new Tile(11, TileType.Property, 150));
    }
}
