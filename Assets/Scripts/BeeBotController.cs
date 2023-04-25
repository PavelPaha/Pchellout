using UnityEngine;

public class BeeBotController : MonoBehaviour
{
    [SerializeField] protected GameObject targetsParent;
    [SerializeField] protected GameObject spawnObject;
    [SerializeField] protected float speed = 3;

    protected const float Delta = 0.1f;

    protected void UpdateAnimationDirection(Vector2 direction) =>
        GetComponent<SpriteRenderer>().flipX = direction.x < 0.01f;

    protected void MoveToTarget(GameObject target)
    {
        var targetPosition = target.transform.position;
        var currentPosition = transform.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        UpdateAnimationDirection(direction);
    }

    protected void MoveToSpawn() => MoveToTarget(spawnObject);
}