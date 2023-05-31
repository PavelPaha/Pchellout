using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Globals
{
    public static bool CameraIsInHive = false;

    public static int ExtractorPrice = 2000;

    public static bool SelectBuildingMode = false;

    public static bool Pause = false;

    public static readonly float FireCooldown = 0.3f;

    public static readonly int ShotCost = 100;

    public static readonly int MaxWobbleAngle = 15;

    public static GameOutcome GameOutcome = GameOutcome.Menu;

    public static float BossChildrenSpawnInterval = 10f;

    public static int BossChildrenCount = 13;

    public static int MaxHiveHealth = 30_000;

    public static int MaxHoneyStorageCapacity = 10_000_000;
    public static int HoneyStorageUpgradePrice = 30_000;
    public static int CurrentStorageCapacity = 1_000_000;

    public static int MaxDefendersUpgrade = 5;
    public static int CurrentDefenderUpgradeLevel = 0;
    public static int DefendersUpgradePrice = 50000;
    public static int DefenderPrice = 2000;
    public static int DefenderDamage = 23;
    public static float DefenderScale = 0.8f;

    public static int FixHivePrice = 80_000;
    public static float FixHiveCoeff = 0.05f;

    public static int MaxProjectileDamage = 100;
    public static int ProjectileUpgradePrice = 5000;
    public static int ProjectileDamage = 30;

    public static int BeeEnemyDamage = 21;

    public static readonly float ProjectileSpeed = 8f;

    public static readonly Dictionary<string, GameResource> GameResources = new()
    {
        { "honey", new GameResource("honey", 172225) }
    };

    public static readonly BuildingData[] BuildigsInWorld =
    {
        new("Колеус", new Dictionary<string, int>
        {
            { "honey", 8000 },
        }, string.Empty),
        new("Ромашка", new Dictionary<string, int>
        {
            { "honey", 8000 },
        }, string.Empty),
        new("Подсолнух", new Dictionary<string, int>
        {
            { "honey", 8000 },
        }, string.Empty),
        new("Куст", new Dictionary<string, int>
        {
            { "honey", 5000 }
        }, string.Empty),
        new("Бомба", new Dictionary<string, int>
            {
                { "honey", 5000 }
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
        new() { EnemyCount = 50, Speed = 5, Scale = 1f, SourceName = "1" },
        new() { EnemyCount = 20, Speed = 5, Scale = 0.7f, BeeSpawnInterval = 0.5f, SourceName = "2" },
        new() { EnemyCount = 180, Speed = 5, Scale = 1f, BeeSpawnInterval = 0.3f, SourceName = "3" },
        new() { EnemyCount = 1, Speed = 5, Scale = 1, SourceName = "4" }
    };

    public static bool InBounds(Vector3 position)
    {
        return position is { x: > -12, y: > -9 } and { x: < 12, y: < 4.5f };
    }

    public static bool InBoundsLowCondition(Vector3 position)
    {
        return false;
        return position is { x: > -12 and < 10, y: > -10 and < 2f };
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