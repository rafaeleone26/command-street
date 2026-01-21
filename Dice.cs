class Dice
{
    private Random _random = new Random();

    public int Roll()
    {
        return _random.Next(1, 7); // 1 to 6
    }
}
