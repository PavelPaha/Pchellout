using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Extractor : BeeBot
{
    [SerializeField] private int nectarCount;
    [SerializeField] private int maxNectarCount;

    private ExtractorState _extractorState;
    [SerializeField] private float _timeSinceExtractingStart;
    [SerializeField] private float _extractingStartTime;
    private List<GameObject> _targets;
    private int _targetIndex;

    // Start is called before the first frame update
    void Start()
    {
        _targets = GetTargets();
        _extractorState = new MovingToTargetState(this);
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceExtractingStart = Time.timeSinceLevelLoad - _extractingStartTime;
        _targets = GetTargets();
        _extractorState.MoveToTarget();
        _extractorState.MoveToSpawn();
        _extractorState.ExtractNectar();
    }

    private List<GameObject> GetTargets() => Enumerable
        .Range(0, targetsParent.transform.childCount)
        .Select(index => targetsParent.transform.GetChild(index).gameObject)
        .Where(target => target.GetComponent<Flower>().lifeStep != LifeStep.Child)
        .ToList();

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
            if (_extractor._targets == null || _extractor._targets.Count == 0)
                return;
            var target = _extractor._targets[_extractor._targetIndex];
            _extractor.MoveToTarget(target);
            if (_extractor.IsAtTargetLocation(target))
            {
                _extractor._targetIndex = (_extractor._targetIndex + 1) % _extractor._targets.Count;
                _extractor._extractorState = new ExtractingNectarState(_extractor);
                _extractor._extractingStartTime = Time.timeSinceLevelLoad;
                _extractor._timeSinceExtractingStart = 0;
            }
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
            if (_extractor._timeSinceExtractingStart > 5)
                _extractor._extractorState = new MovingToSpawn(_extractor);
            //TODO
        }
    }
}