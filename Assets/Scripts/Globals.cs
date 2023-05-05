using System.Collections.Generic;

public static class Globals
{
    public static readonly Dictionary<string, GameResource> GameResources = new()
    {
        { "honey", new GameResource("honey", 0) },
        { "pollen", new GameResource("pollen", 0) }
    };

    public static readonly BuildingData[] BuildingData =
    {
        new("Barrack", new Dictionary<string, int>
        {
            { "honey", 2000 },
            { "pollen", 1000 }
        }),
        new("Pollen Storage", new Dictionary<string, int>
        {
            { "honey", 1500 },
            { "pollen", 500 }
        }),
        new("Honey Storage", new Dictionary<string, int>
        {
            { "honey", 2000 },
            { "pollen", 1000 }
        })
    };
}