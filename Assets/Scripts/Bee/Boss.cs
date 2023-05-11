using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Boss : BeeEnemy
    {
        public GameObject Child;
        float time = 0;
        public float minDelay = 1f;
        public float maxDelay = 5f;
        
        private float delayTimer = 0f;
        private Vector3 originalPosition;
        private Rigidbody2D rb;

        public void Start()
        {
            GetComponent<HealthBarController>().SetMaxHealth(Health);
            originalPosition = transform.position;
            GetComponent<Boss>().BeesSource = GameObject.Find("Hive");
            rb = GetComponent<Rigidbody2D>();
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
            Vector2 direction = GetComponent<Boss>().BeesSource.transform.position - transform.position;
            Vector2 force = direction.normalized * Time.deltaTime * 100;
            float distance = direction.magnitude;
            
            rb.AddForce(force, ForceMode2D.Impulse);
            if (distance > 4f)
            {
                force = direction.normalized * 80 * Time.deltaTime;

                rb.AddForce(force);
                if (delayTimer <= 0f)
                {
                    // Задержка времени после остановки
                    delayTimer = Random.Range(minDelay, maxDelay);

                    // Сбросить направление движения
                    direction = originalPosition - transform.position;
                    rb.velocity = Vector2.zero;
                }
                else
                    delayTimer -= Time.deltaTime;
            }
            else
                rb.AddForce(-direction.normalized * 1.5f, ForceMode2D.Impulse);

            if (!Globals.InBounds(gameObject.transform.position))
            {
                rb.AddForce(direction.normalized * Time.deltaTime * 500);
            }
        }


        private void SpawnChilds(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
                var enemy = Instantiate(Child, transform.position + spawnPosition/4, Quaternion.identity);
                enemy.GetComponent<Rigidbody2D>().AddForce(spawnPosition);
                enemy.GetComponent<BeeEnemy>().BeesSource = GameObject.Find("Hive");
            }
        }
        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            SpawnChilds(2);
            Vector2 direction = GetComponent<Boss>().BeesSource.transform.position - transform.position;
            Vector2 force = direction.normalized*5;
            rb.AddForce(-force);
        }
    }
}