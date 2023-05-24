using System;
using System.Linq;
using DefaultNamespace;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using Object = UnityEngine.Object;

public class Extractor : BasicBee
{
    private ExtractorState _extractorState;
    private NectarInventory _inventory;
    private GameObject _target;
    public GameObject targetsParent;
    public GameObject spawnObject;

    public static Action OnResourcesUpdated;
    
    void Awake()
    {
        _inventory = GetComponent<NectarInventory>();
        _extractorState = new MovingToTargetState(this);
        targetsParent = GameObject.Find("Flowers");
    }
    
    void Update()
    {
        if (!Globals.InBounds(transform.position))
        {
            DestroyBee();
        }
        transform.rotation = Quaternion.identity;
        _extractorState.MoveToTarget();
        _extractorState.MoveToSpawn();
        _extractorState.ExtractNectar();
    }
    
    protected void MoveToSpawn() => MoveToTarget(spawnObject);

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
        try
        {
            _target = Enumerable
                .Range(0, targetsParent.transform.childCount)
                .Select(index => targetsParent.transform.GetChild(index).gameObject)
                .Where(target => target.GetComponent<Flower>().lifeStep != LifeStep.Child)
                .OrderByDescending(target => target.GetComponent<NectarInventory>().NectarCount)
                .FirstOrDefault();
        }
        catch (Exception e)
        {
            throw new NotSupportedException("Extractor не может извлечь компонент из цели во время поиска цели", e);
        }
    }

    private bool IsAtTargetLocation(GameObject target) =>
        ((Vector2)(transform.position - target.transform.position)).magnitude < Delta;

    private bool IsAtSpawnLocation() => IsAtTargetLocation(spawnObject);

    private abstract class ExtractorState
    {
        protected Extractor _extractor;

        protected ExtractorState(Extractor extractor) => _extractor = extractor;

        public abstract void MoveToTarget();
        public abstract void MoveToSpawn();
        public abstract void ExtractNectar();
    }

    private class MovingToTargetState : ExtractorState
    {
        public MovingToTargetState(Extractor extractor) : base(extractor) { }

        public override void MoveToTarget()
        {
            _extractor.UpdateTarget();
            var target = _extractor._target;
            if (target == null || target.transform.childCount == 0)
            {
                _extractor.MoveToSpawn();
                //TODO: если цветов нет, то скорее всего экстрактор должен прилететь в улье и в него залететь (пропасть). Ну или нет:)
                return;
            }

            _extractor.MoveToTarget(target);
            if (_extractor.IsAtTargetLocation(target))
                _extractor._extractorState = new ExtractingNectarState(_extractor);
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
            _extractor.MoveToSpawn();
            if (_extractor.IsAtSpawnLocation())
            {
                _extractor._extractorState = new MovingToTargetState(_extractor);
                Globals.AddHoney(
                    _extractor.gameObject
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
            _extractor.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _extractor._inventory.ExtractNectar(_extractor._target);
            if (_extractor._inventory.IsFull || _extractor._target.GetComponent<NectarInventory>().NectarCount < 10)
                _extractor._extractorState = new MovingToSpawn(_extractor);
        }
    }
}