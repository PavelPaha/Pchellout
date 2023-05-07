using System.Collections.Generic;

public static class Globals
{
    public static readonly Dictionary<string, GameResource> GameResources = new()
    {
        { "honey", new GameResource("honey", 0) },
    };

    public static readonly BuildingData[] Buildings =
    {
        new("Барак", 100, new Dictionary<string, int>
        {
            { "honey", 2000 },
        }),
        new("Медохранилище", 100, new Dictionary<string, int>
        {
            { "honey", 1500 },
        }),
        new("Ратуша", 100, new Dictionary<string, int>
        {
            { "honey", 2000 },
        }),
        new("Колеус", 100, new Dictionary<string, int>
        {
            { "honey", 1000 },
        }),
        new("Ромашка", 100, new Dictionary<string, int>
        {
            { "honey", 1500 },
        }),
        new("Подсолнух", 100, new Dictionary<string, int>
        {
            { "honey", 2000 },
        }),
        new("Куст", 100, new Dictionary<string, int>
        {
            { "honey", 500 },
        })
    };

    public static readonly AttackWave[] AttackWaves =
    {
        new() { EnemyCount = 3, Duration = 5, Speed = 2, Scale = 0.5f, EnemyIndex = 0 },
        new() { EnemyCount = 10, Duration = 10, Speed = 5, Scale = 0.7f, EnemyIndex = 1 },
        new() { EnemyCount = 5000, Duration = 10, Speed = 2, Scale = 1f, EnemyIndex = 2 }
    };
}