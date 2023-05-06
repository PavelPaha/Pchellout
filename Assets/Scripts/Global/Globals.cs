using System.Collections.Generic;

public static class Globals
{
    public const int ProjectileDamage = 20;
    
    public static bool CameraIsInHive = false;
    
    
    public static readonly Dictionary<string, GameResource> GameResources = new()
    {
        { "honey", new GameResource("honey", 0) }
    };

    public static readonly BuildingData[] BuildingData =
    {
        new("Barrack", new Dictionary<string, int>
        {
            { "honey", 2000 }
        }),
        new("Pollen Storage", new Dictionary<string, int>
        {
            { "honey", 1500 }
        }),
        new("Honey Storage", new Dictionary<string, int>
        {
            { "honey", 2000 }
        })
    };
    
    public static AttackWave[] AttackWaves = 
    {
        new() { EnemyCount = 0, Duration = 5, Speed = 2, Scale = 0.5f, EnemyIndex = 0},
        new() { EnemyCount = 0, Duration = 10, Speed = 5, Scale = 0.7f, EnemyIndex = 1},
        new() { EnemyCount = 0, Duration = 10, Speed = 2, Scale = 1f, EnemyIndex = 2}
    };
}