using System.Collections.Generic;
using System.Linq;

public class BuildingData
{
    public readonly string Name;
    public readonly Dictionary<string, int> Cost;

    public BuildingData(string name, Dictionary<string, int> cost)
    {
        Name = name;
        Cost = cost;
    }

    public bool CanBuy() => Cost
        .All(pair => pair.Value < Globals.GameResources[pair.Key].Amount);
}