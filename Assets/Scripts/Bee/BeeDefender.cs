using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class BeeDefender: BasicBee
    {
        public GameObject BeesSource;
        private List<GameObject> _bees;
        
        void Update()
        {
            // TODO пчела, как только разрушит свою цель, ничего не делает.
            // Нужно сделать так, чтобы враг после этого выбирал другую цель и летел к ней
            transform.rotation = Quaternion.identity;
            if (BeesSource == null)
            {
                throw new NullReferenceException("Enemy не знает, на какую цель ему лететь");
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

        private static void DamageInCollisionWithOtherObject(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    collision.gameObject.GetComponent<BeeEnemy>().Damage(20);
                    break;
            }
        }
    }
}