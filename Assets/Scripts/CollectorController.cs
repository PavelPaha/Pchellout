using UnityEngine;

public class CollectorController : BeeBotController
{
    private bool _goToTarget = true;
    private int _targetIndex = 0;

    // Update is called once per frame
    void Update()
    {
        var target = Targets[_targetIndex];
        if (IsAtTargetLocation(target))
            _goToTarget = false;
        if (IsAtSpawnLocation())
        {
            _goToTarget = true;
            _targetIndex = (_targetIndex + 1) % Targets.Count;
        }

        VisitTargetAndReturn(target);
    }

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