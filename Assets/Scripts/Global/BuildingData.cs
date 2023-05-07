using System.Collections.Generic;
using System.Linq;

public class BuildingData
{
    public readonly string Name;
    public readonly int Hp;
    public readonly Dictionary<string, int> Cost;
    public readonly string Description;

    public BuildingData(string name, int hp, Dictionary<string, int> cost, string description)
    {
        Name = name;
        Hp = hp;
        Cost = cost;
        Description = description;
    }

    public bool CanBuy() => Cost
        .All(pair => pair.Value <= Globals.GameResources[pair.Key].Amount);
}