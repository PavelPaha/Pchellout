using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeeBotController : MonoBehaviour
{
    [SerializeField] protected GameObject targetsParent;
    [SerializeField] protected GameObject spawnObject;
    [SerializeField] protected float speed = 3;

    protected List<GameObject> Targets;
    protected SpriteRenderer SpriteRenderer;
    protected const float Delta = 0.1f;

    // Start is called before the first frame update
    protected void Start()
    {
        Targets = GetTargets();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() { }

    protected void UpdateAnimationDirection(Vector2 direction) => SpriteRenderer.flipX = direction.x < 0.01;

    protected void MoveToTarget(GameObject target)
    {
        var targetPosition = target.transform.position;
        var currentPosition = transform.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        UpdateAnimationDirection(direction);
    }

    protected void MoveToSpawn() => MoveToTarget(spawnObject);

    protected List<GameObject> GetTargets() => Enumerable
        .Range(0, targetsParent.transform.childCount)
        .Select(index => targetsParent.transform.GetChild(index).gameObject)
        .ToList();
}