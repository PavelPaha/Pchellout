using System;
using System.Collections.Generic;
using System.Linq;
using Global;
using UnityEngine;
using UnityEngine.Windows;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BeeDefender: BasicBee
    {
        public GameObject BeesSource;
        private List<GameObject> _bees;
        private GameObject _goal;

        public void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (!Globals.InBounds(transform.position))
                DestroyObject();
            // TODO пчела, как только разрушит свою цель, ничего не делает.
            // Нужно сделать так, чтобы враг после этого выбирал другую цель и летел к ней
            transform.rotation = Quaternion.identity;
            if (BeesSource == null)
            {
                throw new NullReferenceException("Enemy не знает, на какую цель ему лететь");
            }
            
            if (_goal == null || _goal.name == "Hive")
            {
                _bees = Enumerable
                    .Range(0, BeesSource.transform.childCount)
                    .Select(index => BeesSource.transform.GetChild(index).gameObject)
                    .ToList();

                if (_bees.Count > 0)
                    _goal = _bees[Random.Range(0, _bees.Count)];
                else
                    _goal = GameObject.Find("Hive");
            }
            MoveToTarget(_goal);
            
            if (!Globals.InBoundsLowCondition(gameObject.transform.position))
            {
                Vector2 direction = BeesSource.transform.position - transform.position;
                GetComponent<Rigidbody2D>().AddForce(direction.normalized * Time.deltaTime * 1000);
            }
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
                case "Enemy":
                    collision.gameObject.GetComponent<BeeEnemy>().Damage(Globals.DefenderDamage);
                    _audioSource.Play();
                    break;
                case "Boss":
                    collision.gameObject.GetComponent<Boss>().Damage(Globals.DefenderDamage);
                    _audioSource.Play();
                    break;
            }
        }
        
        public override void ShowName()
        {
            OnNotify?.Invoke(gameObject, $"Защитник.\n Сила - {Globals.BeeEnemyDamage}");
        }
    }
    
    
}