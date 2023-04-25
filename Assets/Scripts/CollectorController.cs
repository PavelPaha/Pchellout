using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectorController : BeeBotController
{
    private bool _goToTarget = true;
    private List<GameObject> _targets;
    private int _targetIndex;

    // Start is called before the first frame update
    void Start() => _targets = GetTargets();

    // Update is called once per frame
    void Update()
    {
        var target = _targets[_targetIndex];
        if (IsAtTargetLocation(target))
            _goToTarget = false;
        if (IsAtSpawnLocation())
        {
            _goToTarget = true;
            _targetIndex = (_targetIndex + 1) % _targets.Count;
        }

        VisitTargetAndReturn(target);
    }

    private List<GameObject> GetTargets() => Enumerable
        .Range(0, targetsParent.transform.childCount)
        .Select(index => targetsParent.transform.GetChild(index).gameObject)
        .ToList();

    private void VisitTargetAndReturn(GameObject target)
    {
        if (_goToTarget)
            MoveToTarget(target);
        else
            MoveToSpawn();
    }

    private bool IsAtTargetLocation(GameObject target) =>
        ((Vector2)(transform.position - target.transform.position)).magnitude < Delta;

    private bool IsAtSpawnLocation() => IsAtTargetLocation(spawnObject);
}