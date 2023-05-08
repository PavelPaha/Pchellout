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

    private float frame = 0f;

    protected void UpdateAnimationDirection(Rigidbody2D rigidbody) =>
        GetComponent<SpriteRenderer>().flipX = rigidbody.velocity.x < 0.01f;

    protected void MoveToTarget(GameObject target)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector2 targetPosition = target.transform.position;
        Vector2 currentPosition = rigidbody.position;
        Vector2 directionToTarget = (targetPosition - currentPosition).normalized;

        // Вычисляем случайное отклонение от направления к цели
        frame = frame < 100 ? frame + 0.01f : 0;
        float wobbleAngle = UnityEngine.Random.Range(-Globals.MaxWobbleAngle, Globals.MaxWobbleAngle);
        var wobbleRotation = Quaternion.Euler(0f, 0f, (float)Math.Sin(frame) * Globals.MaxWobbleAngle / 3 + wobbleAngle);

        // Получаем новое направление на основе отклонения
        Vector2 wobbledDirection = wobbleRotation * directionToTarget;
        
        Vector2 moveForce = wobbledDirection * Speed - rigidbody.velocity;
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
        if (!Globals.InBounds(transform.position))
        {
            DestroyBee();
        }
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
