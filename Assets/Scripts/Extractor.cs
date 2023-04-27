using System.Linq;
using UnityEngine;

public class Extractor : BeeBot
{
    private ExtractorState _extractorState;
    private NectarInventory _inventory;
    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        _extractorState = new MovingToTargetState(this);
        _inventory = GetComponent<NectarInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        _extractorState.MoveToTarget();
        _extractorState.MoveToSpawn();
        _extractorState.ExtractNectar();
    }

    private void UpdateTarget()
    {
        _target = Enumerable
            .Range(0, targetsParent.transform.childCount)
            .Select(index => targetsParent.transform.GetChild(index).gameObject)
            .Where(target => target.GetComponent<Flower>().lifeStep != LifeStep.Child)
            .OrderByDescending(target => target.GetComponent<NectarInventory>().NectarCount)
            .FirstOrDefault();
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
            if (target == null)
                return;
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
                _extractor._extractorState = new MovingToTargetState(_extractor);
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
            if (_extractor._inventory.IsFull || _extractor._target.GetComponent<NectarInventory>().NectarCount < 10)
                _extractor._extractorState = new MovingToSpawn(_extractor);

            _extractor._inventory.ExtractNectar(_extractor._target);
        }
    }
}