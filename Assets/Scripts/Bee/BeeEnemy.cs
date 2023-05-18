using System;
using System.Collections.Generic;
using System.Linq;
using Global;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class BeeEnemy: BasicBee
    {
        public GameObject BeesSource;
        private List<GameObject> _bees;
        private Queue<GameObject> _sources = new();
        private Organizer _organizer;

        public void Start()
        {
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
                    collision.gameObject.GetComponent<HouseForBees>().Damage(20);
                    break;
                case "Extractor":
                    collision.gameObject.GetComponent<Extractor>().Damage(20);
                    break;
                case "Flower":
                    collision.gameObject.GetComponent<Flower>().Damage(20);
                    break;
                case "Defender":
                    BeesSource = collision.transform.parent.gameObject;
                    collision.gameObject.GetComponent<BeeDefender>().Damage(20);
                    break;
            }
        }
    }
}