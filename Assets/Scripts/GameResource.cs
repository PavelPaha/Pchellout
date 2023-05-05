using System;

public class GameResource
{
    public readonly string Name;

    public int Amount
    {
        get => Amount;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            Amount = value;
        }
    }

    public GameResource(string name, int initialAmount)
    {
        Name = name;
        Amount = initialAmount;
    }
}