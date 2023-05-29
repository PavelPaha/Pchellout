using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Boss : BeeEnemy
    {
        public GameObject Child;
        float time = 0;

        private float delayTimer = 0f;
        private Rigidbody2D rb;


        public AudioClip BackgroundBossSound;

        public void Awake()
        {
            GetComponent<Boss>().BeesSource = GameObject.Find("Hive");
            rb = GetComponent<Rigidbody2D>();
            var audioController = GameObject.Find("Audio");
            audioController.GetComponent<AudioSource>().clip = BackgroundBossSound;
            audioController.GetComponent<AudioSource>().Play();
        }

        public override void Update()
        {
            Vector2 direction = GetComponent<Boss>().BeesSource.transform.position - transform.position;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = direction.x < 0.01f;
            gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().flipX = direction.x < 0.01f;
            
            transform.rotation = Quaternion.identity;
            if (BeesSource == null)
            {
                throw new NullReferenceException("Boss не знает, на какую цель ему лететь");
            }
            
            Move();
            RefreshTime();
        }

        private void RefreshTime()
        {
            time += Time.deltaTime;
            if (time >= Globals.BossChildrenSpawnInterval)
            {
                time = 0;
                SpawnChilds(Globals.BossChildrenCount);
            }
        }

        private void Move()
        {
            Vector2 direction = BeesSource.transform.position - transform.position;
            Vector2 force = direction.normalized * Time.deltaTime * 100;
            force = direction.normalized * 1000 * Time.deltaTime;
            rb.AddForce(force);

            if (!Globals.InBounds_LowCondition(gameObject.transform.position))
            {
                rb.AddForce(direction.normalized * Time.deltaTime * 80000);
            }
        }


        private void SpawnChilds(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
                var enemy = Instantiate(Child, transform.position + spawnPosition/4, Quaternion.identity);
                enemy.transform.SetParent(GameObject.Find("Enemies").transform);
                enemy.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                enemy.GetComponent<BasicBee>().Health = 300;
                enemy.GetComponent<Rigidbody2D>().AddForce(spawnPosition);
                enemy.GetComponent<BeeEnemy>().BeesSource = GameObject.Find("Hive");
            }
        }
        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (Random.Range(1,5) < 2)
                SpawnChilds(2);
            Vector2 direction = GetComponent<Boss>().BeesSource.transform.position - transform.position;
            Vector2 force = direction.normalized*5;
            rb.AddForce(-force);
        }
    }
}