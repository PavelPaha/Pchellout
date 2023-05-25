using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Extractor : BasicBee
{
    private ExtractorState _extractorState;
    private NectarInventory _inventory;
    private GameObject _target;
    public GameObject targetsParent;
    public GameObject spawnObject;

    public static Action OnResourcesUpdated;

    public void Awake()
    {
        spawnObject = GameObject.Find("Hive");
        _inventory = GetComponent<NectarInventory>();
        _extractorState = new MovingToSpawn(this);
        targetsParent = GameObject.Find("Flowers");
    }

    private void Update()
    {
        if (!Globals.InBounds(transform.position))
        {
            DestroyObject();
        }

        transform.rotation = Quaternion.identity;
        _extractorState.MoveToTarget();
        _extractorState.MoveToSpawn();
        _extractorState.ExtractNectar();
    }

    private void MoveToSpawn() => MoveToTarget(spawnObject);

    public void OnCollisionEnter2D(Collision2D collision)
    {
        DamageInCollisionWithEnemy(collision);
    }

    public void OnCollisionStay2D(Collision2D collisionInfo)
    {
        DamageInCollisionWithEnemy(collisionInfo);
    }

    private static void DamageInCollisionWithEnemy(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                collision.gameObject.GetComponent<BeeEnemy>().Damage(20);
                break;
            case "Boss":
                collision.gameObject.GetComponent<Boss>().Damage(20);
                break;
        }
    }
    
    private void UpdateTarget()
    {
        var potentialTargets = Enumerable
            .Range(0, targetsParent.transform.childCount)
            .Select(index => targetsParent.transform.GetChild(index).gameObject)
            .Where(target => target.GetComponent<Flower>().lifeStep != LifeStep.Child)
            .ToList();
        var randomIndex = Random.Range(0, potentialTargets.Count);
        _target = potentialTargets.Count > 0 ? potentialTargets[randomIndex] : null;
    }

    private bool IsAtTargetLocation(GameObject target) =>
        ((Vector2)(transform.position - target.transform.position)).magnitude < Delta;

    private bool IsAtSpawnLocation() => IsAtTargetLocation(spawnObject);

    private abstract class ExtractorState
    {
        protected readonly Extractor Extractor;

        protected ExtractorState(Extractor extractor) => Extractor = extractor;

        public abstract void MoveToTarget();
        public abstract void MoveToSpawn();
        public abstract void ExtractNectar();
    }

    private class MovingToTargetState : ExtractorState
    {
        public MovingToTargetState(Extractor extractor) : base(extractor) { }

        public override void MoveToTarget()
        {
            var target = Extractor._target;
            if (target is null)
            {
                Extractor.MoveToSpawn();
                return;
            }

            Extractor.MoveToTarget(target);
            if (Extractor.IsAtTargetLocation(target))
                Extractor._extractorState = new ExtractingNectarState(Extractor);
        }

        public override void MoveToSpawn() { }

        public override void ExtractNectar() { }
    }

    private class MovingToSpawn : ExtractorState
    {
        public MovingToSpawn(Extractor extractor) : base(extractor) { }

        public override void MoveToTarget() { }

        public override void MoveToSpawn()
        {
            Extractor.MoveToSpawn();
            if (Extractor.IsAtSpawnLocation())
            {
                Extractor.UpdateTarget();
                Extractor._extractorState = new MovingToTargetState(Extractor);
                Globals.AddHoney(Extractor.gameObject
                    .GetComponent<NectarInventory>()
                    .DeliverNectar(int.MaxValue)
                );
                OnResourcesUpdated();
            }
        }

        public override void ExtractNectar() { }
    }

    private class ExtractingNectarState : ExtractorState
    {
        public ExtractingNectarState(Extractor extractor) : base(extractor) { }

        public override void MoveToTarget() { }

        public override void MoveToSpawn() { }

        public override void ExtractNectar()
        {
            Extractor.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Extractor._inventory.ExtractNectar(Extractor._target);
            if (Extractor._inventory.IsFull || Extractor._target.GetComponent<NectarInventory>().NectarCount < 10)
                Extractor._extractorState = new MovingToSpawn(Extractor);
        }
    }
}