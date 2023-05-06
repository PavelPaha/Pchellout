using System;

public class GameResource
{
    public readonly string Name;
    private int _amount;

    public int Amount
    {
        get => _amount;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _amount = value;
        }
    }


    public GameResource(string name, int initialAmount)
    {
        Name = name;
        Amount = initialAmount;
    }
}