using System.Collections.Generic;

public static class Globals
{
    public static readonly Dictionary<string, GameResource> GameResources = new()
    {
        { "honey", new GameResource("honey", 0) },
    };

    public static readonly BuildingData[] Buildings =
    {
        new("ColeusFlower", 100, new Dictionary<string, int>
        {
            { "honey", 2000 },
        }),
        new("DaisyFlower", 100, new Dictionary<string, int>
        {
            { "honey", 1500 },
        }),
        new("SunflowerFlower", 100, new Dictionary<string, int>
        {
            { "honey", 2000 },
        })
    };

    public static readonly AttackWave[] AttackWaves =
    {
        new() { EnemyCount = 3, Duration = 5, Speed = 2, Scale = 0.5f, EnemyIndex = 0 },
        new() { EnemyCount = 10, Duration = 10, Speed = 5, Scale = 0.7f, EnemyIndex = 1 },
        new() { EnemyCount = 5000, Duration = 10, Speed = 2, Scale = 1f, EnemyIndex = 2 }
    };
}