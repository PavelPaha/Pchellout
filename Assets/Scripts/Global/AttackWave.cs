using UnityEngine;

/// <summary>
/// Это класс волны нападения, который хранит параметры для врага,
/// которого будет генерировать EnemiesSpawner
/// </summary>
public class AttackWave
{
    //TODO перенести этот класс в Locals
    public int EnemyCount;
    public int Speed;
    public float Scale = 1;
    public float BeeSpawnInterval = 1f;
    public string SourceName;
}