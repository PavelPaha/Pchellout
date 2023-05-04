using System;
using UnityEngine;
using Random = System.Random;

public class BeeBot : MonoBehaviour
{
    // [SerializeField] protected GameObject targetsParent;
    // [SerializeField] protected GameObject spawnObject;
    // [SerializeField] protected float speed = 3;
    //
    // protected const float Delta = 0.1f;
    //
    // protected void UpdateAnimationDirection(Vector2 direction) =>
    //     GetComponent<SpriteRenderer>().flipX = direction.x < 0.01f;
    //
    // protected void MoveToTarget(GameObject target)
    // {
    //     Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    //     Vector2 targetPosition = target.transform.position;
    //     Vector2 currentPosition = rigidbody.position;
    //     Vector2 direction = (targetPosition - currentPosition).normalized;
    //     Vector2 moveForce = direction * speed - rigidbody.velocity;
    //     rigidbody.AddForce(moveForce, ForceMode2D.Impulse);
    // }
    //
    // protected void MoveToSpawn() => MoveToTarget(spawnObject);
}