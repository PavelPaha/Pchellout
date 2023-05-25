using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Globals
{
    public static bool CameraIsInHive = false;

    public static int ExtractorPrice = 2000;

    public static bool Pause = false;

    public static readonly float FireCooldown = 0.3f;

    public static readonly int ShotCost = 0;

    public static readonly int MaxWobbleAngle = 15;

    public static GameOutcome GameOutcome = GameOutcome.Win;

    public static float BossChildrenSpawnInterval = 10f;

    public static int BossChildrenCount = 5;

    public static int MaxHiveHealth = 10000;

    public static int MaxHoneyStorageCapacity = 10_000_000;
    public static int HoneyStorageUpgradePrice = 1_000;
    public static int CurrentStorageCapacity = 1_000_000;

    public static int MaxDefendersUpgrade = 5;
    public static int CurrentDefenderUpgradeLevel = 0;
    public static int DefendersUpgradePrice = 1000;
    public static int DefenderCost = 500;

    public static int BombCapacity = 5;
    public static int BombPrice = 1000;
    public static int CurrentBombCount = 0;

    public static int FixHivePrice = 25000;
    public static float FixHiveCoeff = 0.05f;

    public static int MaxProjectileDamage = 100;
    public static int ProjectileUpgradePrice = 1000;
    public static int ProjectileDamage = 20;

    public static readonly float ProjectileSpeed = 8f;

    public static readonly Dictionary<string, GameResource> GameResources = new()
    {
        { "honey", new GameResource("honey", 100) }
    };

    public static readonly BuildingData[] BuildingsInHive =
    {
        new("Барак", new Dictionary<string, int>
        {
            { "honey", 2000 },
        }, string.Empty),
        new("Медохранилище", new Dictionary<string, int>
        {
            { "honey", 1500 },
        }, string.Empty),
        new("Ратуша", new Dictionary<string, int>
        {
            { "honey", 2000 },
        }, string.Empty)
    };

    public static readonly BuildingData[] BuildigsInWorld =
    {
        new("Колеус", new Dictionary<string, int>
        {
            { "honey", 10000 },
        }, string.Empty),
        new("Ромашка", new Dictionary<string, int>
        {
            { "honey", 15000 },
        }, string.Empty),
        new("Подсолнух", new Dictionary<string, int>
        {
            { "honey", 20000 },
        }, string.Empty),
        new("Куст", new Dictionary<string, int>
        {
            { "honey", 5000 }
        }, string.Empty),
        new("Бомба", new Dictionary<string, int>
            {
                { "honey", 100 }
            },
            string.Empty)
    };

    public static readonly Dictionary<string, BuildingData[]> Buildings = new()
    {
        { "hive", BuildigsInWorld },
        { "world", BuildigsInWorld }
    };

    public static readonly AttackWave[] AttackWaves =
    {
        new() { EnemyCount = 20, Speed = 5, Scale = 1f, SourceName = "1" },
        new() { EnemyCount = 10, Speed = 5, Scale = 0.7f, BeeSpawnInterval = 0.4f, SourceName = "2" },
        new() { EnemyCount = 30, Speed = 5, Scale = 1f, BeeSpawnInterval = 0.2f, SourceName = "3" },
        new() { EnemyCount = 1, Speed = 5, Scale = 1, SourceName = "4" },
        new() { EnemyCount = 10, Speed = 10, Scale = 1, BeeSpawnInterval = 15f, SourceName = "4" },
        new() { EnemyCount = 10, Speed = 10, Scale = 1, BeeSpawnInterval = 10f, SourceName = "4" },
        new() { EnemyCount = 10, Speed = 10, Scale = 1, BeeSpawnInterval = 5f, SourceName = "4" }
    };

    public static bool InBounds(Vector3 position)
    {
        return position.x > -12
               && position.y > -9
               && position.x < 12
               && position.y < 4.5f;
    }

    public static void AddHoney(int value)
    {
        GameResources["honey"].Amount = Math.Min(
            GameResources["honey"].Amount + value,
            CurrentStorageCapacity);
    }

    public static void UpdateBuyPossibility(Button button, int price)
    {
        var currentColor = button.GetComponent<Image>().color;
        var possible = price <= Globals.GameResources["honey"].Amount;
        button.GetComponent<Image>().color =
            new Color(
                currentColor.r,
                currentColor.g,
                currentColor.b,
                possible ? 1 : 0.2f);
        button.GetComponent<Button>().enabled = possible;
    }
}

public enum GameOutcome
{
    Loss,
    Win,
    Menu
}