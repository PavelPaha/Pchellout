using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Это класс волны нападения, который хранит параметры для врага,
/// которого будет генерировать EnemiesSpawner
/// </summary>
public class AttackWave
{
    //TODO перенести этот класс в Locals
    public int EnemyCount;
    public float Duration;
    public int Speed;
    public float Scale = 1;
    public int EnemyIndex;
}

/// <summary>
/// Генерирует врагов (EnemyItem) рандомным образом
/// из разных точек, которые берёт из HoneycombObjectЮ
/// которые летят на Goals рандомно
/// При этом это происходит "волновым" образом
/// </summary>
public class EnemiesSpawner : MonoBehaviour
{
    public List<GameObject> Goals;
    public List<GameObject> EnemyItems;
    public GameObject HoneycombObject;
    public float BeeSpawnInterval = 5f;
    public List<AttackWave> _attackWaves = new()
    {
        new AttackWave { EnemyCount = 3, Duration = 5, Speed = 2, Scale = 0.5f, EnemyIndex = 0},
        new AttackWave { EnemyCount = 10, Duration = 10, Speed = 5, Scale = 0.7f, EnemyIndex = 1},
        new AttackWave { EnemyCount = 5000, Duration = 10, Speed = 2, Scale = 1f, EnemyIndex = 2}
    };
    
    private Transform[] _honeyCombs;
    private float _timer;
    private int _currentWaveIndex;
    private int _beesToSpawn;
    private float _waveTimer;
    private float _beeSpawnTimer;

    void Start()
    {
        _honeyCombs = HoneycombObject.GetComponentsInChildren<Transform>();
        _beesToSpawn = _attackWaves[_currentWaveIndex].EnemyCount;
    }

    void Update()
    {
        IterateWave();
        if (_beeSpawnTimer >= BeeSpawnInterval && _beesToSpawn > 0)
        {
            SpawnEnemy();
            _timer = 0f;
            _beesToSpawn--;
            _beeSpawnTimer = 0f;
        }
    }

    /// <summary>
    /// Делает так, чтобы _currentWaveIndex указывал на те параметры
    /// из _attackWaves[_currentWaveIndex],
    /// которые нужно применить к врагам в данный момент
    /// </summary>
    private void IterateWave()
    {
        _waveTimer += Time.deltaTime;
        if (_waveTimer >= _attackWaves[_currentWaveIndex].Duration)
        {
            _waveTimer = 0f;
            _currentWaveIndex++;
            _currentWaveIndex = (_currentWaveIndex + 1) % _attackWaves.Count;
            _beesToSpawn = _attackWaves[_currentWaveIndex].EnemyCount;
            _beeSpawnTimer = BeeSpawnInterval;
        }
        _beeSpawnTimer += Time.deltaTime;
    }

    private void SpawnEnemy()
    {
        Transform honeycomb = _honeyCombs[Random.Range(1, _honeyCombs.Length-1)];
        var speed = _attackWaves[_currentWaveIndex].Speed;
        var scale = _attackWaves[_currentWaveIndex].Scale;
        var enemyItem = EnemyItems[_attackWaves[_currentWaveIndex].EnemyIndex];
        var newEnemy = Instantiate(enemyItem, honeycomb.position, Quaternion.identity);

        newEnemy.GetComponent<BeeEnemy>().Speed = 
            Random.Range(Math.Max(0, speed - 2), speed + 2);
        newEnemy.transform.localScale = 
            new Vector3(scale, scale, scale);
        
        //TODO исправить баг в присваивании врагу цели (иногда в BeeSource после присваивания лежит null)
        newEnemy.GetComponent<BeeEnemy>().BeesSource = GetRandomObject();
    }

    private GameObject GetRandomObject()
        => Goals[Random.Range(0, Goals.Count)];
}
