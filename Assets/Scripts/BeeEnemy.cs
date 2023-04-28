using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class BeeEnemy: BasicBee
    {
        public GameObject BeesSource;
        private List<GameObject> _bees;
        
        void Update()
        {
            transform.rotation = Quaternion.identity;
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
        
            MoveToTarget(closestTarget);
        }
        
        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            DamageInCollisionWithBeeCollector(collision);
        }
        
        public void OnCollisionStay2D(Collision2D collisionInfo)
        {
            DamageInCollisionWithBeeCollector(collisionInfo);
        }

        private static void DamageInCollisionWithBeeCollector(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("BeeCollector")) // проверяем тег объекта, столкнувшегося с нашим
            {
                collision.gameObject.GetComponent<BasicBee>().Damage(1);
            }
        }
    }
}