using System.Collections.Generic;
using System.Linq;

public class BuildingData
{
    public readonly string Name;
    public readonly Dictionary<string, int> Cost;
    public readonly string Description;

    public BuildingData(string name, Dictionary<string, int> cost, string description)
    {
        Name = name;
        Cost = cost;
        Description = description;
    }

    public bool CanBuy() => Cost
        .All(pair => pair.Value <= Globals.GameResources[pair.Key].Amount);
}