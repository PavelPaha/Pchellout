using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AttackWave
{
    public int BeeCount;
    public float Duration;
}
public class EnemiesSpawner : MonoBehaviour
{

    public GameObject EnemyItem;
    public GameObject HoneycombObject; 
    private Transform[] honeyCombs;

    private static List<GameObject> Bees = new (3);

    public List<AttackWave> _attackWaves = new()
    {
        new AttackWave { BeeCount = 3, Duration = 5 },
        new AttackWave { BeeCount = 10, Duration = 10 },
        new AttackWave { BeeCount = 50, Duration = 4 },
    };
    private float timer = 0f;

    // Start is called before the first frame update
    private int currentWaveIndex = 0;
    private int beesToSpawn = 0;

    private float waveTimer = 0f;
    private float beeSpawnTimer = 0f;

    public float BeeSpawnInterval = 0.5f;

    void Start()
    {
        honeyCombs = HoneycombObject.GetComponentsInChildren<Transform>();
        beesToSpawn = _attackWaves[currentWaveIndex].BeeCount;
    }

    void Update()
    {
        waveTimer += Time.deltaTime;

        if (waveTimer >= _attackWaves[currentWaveIndex].Duration)
        {
            waveTimer = 0f;

            currentWaveIndex++;
            currentWaveIndex = (currentWaveIndex + 1) % _attackWaves.Count;
            beesToSpawn = _attackWaves[currentWaveIndex].BeeCount;
            beeSpawnTimer = BeeSpawnInterval;
        }

        beeSpawnTimer += Time.deltaTime;

        if (beeSpawnTimer >= BeeSpawnInterval && beesToSpawn > 0)
        {
            Transform honeycomb = honeyCombs[Random.Range(1, honeyCombs.Length)];
            Instantiate(EnemyItem, honeycomb.position, Quaternion.identity);
            EnemyItem.GetComponent<BeeEnemy>().Speed = Random.Range(1, 10);
            timer = 0f;
            beesToSpawn--;
            beeSpawnTimer = 0f;
        }
        
        
    }
}
