using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBee : MonoBehaviour
{
    public int CollisionForce = 20;
    public int Health = 100;
    private HealthBarController healthBarController;
    [SerializeField] protected GameObject targetsParent;
    [SerializeField] protected GameObject spawnObject;
    [SerializeField] protected float speed = 3;

    protected const float Delta = 0.1f;

    protected void UpdateAnimationDirection(Vector2 direction) =>
        GetComponent<SpriteRenderer>().flipX = direction.x < 0.01f;

    protected void MoveToTarget(GameObject target)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector2 targetPosition = target.transform.position;
        Vector2 currentPosition = rigidbody.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;
        Vector2 moveForce = direction * speed - rigidbody.velocity;
        rigidbody.AddForce(moveForce, ForceMode2D.Impulse);
    }

    protected void MoveToSpawn() => MoveToTarget(spawnObject);
    
    void Start()
    {
        healthBarController = GetComponent<HealthBarController>();
        healthBarController.Health = Health;
    }

    void OnMouseDown(int damage = 20)
    {
        Damage(damage);
    }

    public void Damage(int damage)
    {
        Health -= damage;
        healthBarController.DamageHealth(damage);
        if (Health <= 0)
        {
            DestroyBee();
        }
    }

    public void DestroyBee()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        PushAway(collision);
    }
    public void OnCollisionStay2D(Collision2D collisionInfo)
    {
        PushAway(collisionInfo);
    }

    private void PushAway(Collision2D collision)
    {
        var rb = GetComponent<Rigidbody2D>();
        Vector3 normal = collision.contacts[0].normal;
        rb.AddForce(normal * CollisionForce, ForceMode2D.Impulse);
    }


}
