using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
            DamageInCollisionWithExtractor(collision);
        }
        
        public void OnCollisionStay2D(Collision2D collisionInfo)
        {
            //DamageInCollisionWithExtractor(collisionInfo);
        }
        

        private static void DamageInCollisionWithExtractor(Collision2D collision)
        {
            // throw new Exception(collision.gameObject.tag);
            // if (collision.gameObject.CompareTag("Extractor")) // проверяем тег объекта, столкнувшегося с нашим
            // {
            try
            {
                collision.gameObject.GetComponent<Extractor>().Damage(20);
            }
            catch
            {
                
                throw new Exception($"BeeEnemy столкнулся с объектом, у которого такие компоненты (но у него нет компонента Extractor):  {String.Join(", ", collision.gameObject.GetComponents<Component>().Select(a => a.ToString()))}");
            }
        }
    }
}