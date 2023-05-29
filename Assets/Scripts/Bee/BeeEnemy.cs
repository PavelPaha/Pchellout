using System;
using System.Collections.Generic;
using System.Linq;
using Global;
using UnityEngine;

namespace DefaultNamespace
{
    public class BeeEnemy: BasicBee
    {
        public static Action OnHiveDamage;
        
        public GameObject BeesSource;
        private List<GameObject> _bees;
        private Queue<GameObject> _sources = new();
        private Organizer _organizer;

        public void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _organizer = GameObject.Find("Game").GetComponent<Organizer>();
            AddSources();
            BeesSource = _sources.Dequeue();
        }

        private void AddSources()
        {
            if (_organizer.Flowers.transform.childCount > 0)
                _sources.Enqueue(_organizer.Flowers);
            if (_organizer.Extractors.transform.childCount > 0)
                _sources.Enqueue(_organizer.Extractors);
            if (_organizer.Hive.transform.childCount > 0)
                _sources.Enqueue(_organizer.Hive);
        }

        public virtual void Update()
        {
            // TODO пчела враг, как только разрушит свою цель, ничего не делает.
            // Нужно сделать так, чтобы враг после этого выбирал другую цель и летел к ней
            transform.rotation = Quaternion.identity;
            if (BeesSource == null || BeesSource.transform.childCount == 0)
            {
                if (_sources.Count == 0)
                {
                    AddSources();
                }
                BeesSource = _sources.Dequeue();
            }
            
            _bees = Enumerable
                .Range(0, BeesSource.transform.childCount)
                .Select(index => BeesSource.transform.GetChild(index).gameObject)
                .ToList();

            GameObject closestTarget = null;
            float closestDistance = Mathf.Infinity;
        
            foreach (GameObject target in _bees)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestTarget = target;
                    closestDistance = distance;
                }
            }

            if (closestTarget == null)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                return;
            }
            
            if (!Globals.InBounds_LowCondition(gameObject.transform.position))
            {
                Vector2 direction = BeesSource.transform.position - transform.position;
                GetComponent<Rigidbody2D>().AddForce(direction.normalized * Time.deltaTime * 1000);
            }
        
            MoveToTarget(closestTarget);
        }
        
        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            DamageInCollisionWithOtherObject(collision);
        }
        
        public void OnCollisionStay2D(Collision2D collisionInfo)
        {
            //DamageInCollisionWithExtractor(collisionInfo);
        }
        

        private void DamageInCollisionWithOtherObject(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Hive":
                    OnHiveDamage?.Invoke();
                    collision.gameObject.GetComponent<HouseForBees>().Damage(Globals.BeeEnemyDamage);
                    _audioSource.Play();
                    break;
                case "Extractor":
                    collision.gameObject.GetComponent<Extractor>().Damage(Globals.BeeEnemyDamage);
                    _audioSource.Play();
                    break;
                case "Flower":
                    var flower = collision.gameObject.GetComponent<Flower>();
                    flower.Damage(Globals.BeeEnemyDamage);
                    break;
                case "Defender":
                    BeesSource = collision.transform.parent.gameObject;
                    collision.gameObject.GetComponent<BeeDefender>().Damage(Globals.BeeEnemyDamage);
                    _audioSource.Play();
                    break;
            }
        }

        public override void ShowName()
        {
            OnNotify?.Invoke(gameObject, $"Враг\n Сила - {Globals.BeeEnemyDamage}");
        }
    }
}