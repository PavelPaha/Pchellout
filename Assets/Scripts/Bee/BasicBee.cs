using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicBee : MonoBehaviour
{
    public int Health = 100;
    [SerializeField] public float Speed = 3;

    protected const float Delta = 0.1f;

    protected void UpdateAnimationDirection(Rigidbody2D rigidbody) =>
        GetComponent<SpriteRenderer>().flipX = rigidbody.velocity.x < 0.01f;

    protected void MoveToTarget(GameObject target)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector2 targetPosition = target.transform.position;
        Vector2 currentPosition = rigidbody.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;
        Vector2 moveForce = direction * Speed - rigidbody.velocity;
        rigidbody.AddForce(moveForce, ForceMode2D.Impulse);
        UpdateAnimationDirection(rigidbody);
    }

    void Start()
    {
        GetComponent<HealthBarController>().SetMaxHealth(Health);
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            DestroyBee();
        }
    }

    public void DestroyBee()
    {
        Destroy(gameObject);
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
        // var rb = GetComponent<Rigidbody2D>();
        // Vector3 normal = collision.contacts[0].normal;
        // rb.AddForce(normal * CollisionForce, ForceMode2D.Impulse);
    }


}
