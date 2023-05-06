using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static bool CameraIsInHive = false;
    
    public static readonly int ProjectileDamage = 20;

    public static readonly float FireCooldown =  0.2f;

    public static readonly float ProjectileSpeed = 2f;

    public static readonly int ShotCost = 400;
    
    
    public static readonly Dictionary<string, GameResource> GameResources = new()
    {
        { "honey", new GameResource("honey", 0) }
    };

    public static readonly BuildingData[] BuildingData =
    {
        new("Barrack", new Dictionary<string, int>
        {
            { "honey", 2000 }
        },
            "Это казарма"),
        new("Honey Storage", new Dictionary<string, int>
        {
            { "honey", 2000 }
        }, "Это хранилище мёда"),
        new("TownHall", new Dictionary<string, int>
        {
            { "honey", 5000 }
        }, "Это ратуша")
    };
    
    public static AttackWave[] AttackWaves = 
    {
        new() { EnemyCount = 10, Duration = 15, Speed = 2, Scale = 0.5f, EnemyIndex = 0},
        new() { EnemyCount = 20, Duration = 20, Speed = 5, Scale = 0.6f, EnemyIndex = 1},
        new() { EnemyCount = 30, Duration = 20, Speed = 7, Scale = 0.8f, EnemyIndex = 2}
    };
    
    public static bool InBounds(Vector3 position)
    {
        return position.x > -12
               && position.y> -9 
               && position.x < 12 
               && position.y < 4.5f;
    }
}