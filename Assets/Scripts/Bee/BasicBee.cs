using System;
using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class BasicBee : MonoBehaviour, IPointerEnterHandler
{
    public static Action<GameObject, string> OnNotify;
    
    public float scaleDuration = 1.0f;
    public float maxScale = 1.0f;
    public int Health = 100;
    public float Speed = 3;

    protected const float Delta = 0.1f;

    private Vector3 _originalScale;
    private float frame = 0f;

    protected AudioSource _audioSource;

    protected void UpdateAnimationDirection(Rigidbody2D rigidbody) =>
        GetComponent<SpriteRenderer>().flipX = rigidbody.velocity.x < 0.01f;

    protected void MoveToTarget(GameObject target)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector2 targetPosition = target.transform.position;
        Vector2 currentPosition = rigidbody.position;
        Vector2 directionToTarget = (targetPosition - currentPosition).normalized;
        frame = frame < 100 ? frame + 0.01f : 0;
        float wobbleAngle = UnityEngine.Random.Range(-Globals.MaxWobbleAngle, Globals.MaxWobbleAngle);
        var wobbleRotation =
            Quaternion.Euler(0f, 0f, (float)Math.Sin(frame) * Globals.MaxWobbleAngle / 3 + wobbleAngle);
        Vector2 wobbledDirection = wobbleRotation * directionToTarget;

        Vector2 moveForce = wobbledDirection * Speed - rigidbody.velocity;
        rigidbody.AddForce(moveForce, ForceMode2D.Impulse);
        UpdateAnimationDirection(rigidbody);
    }

    void Start()
    {
        _audioSource = GetComponents<AudioSource>()[0];
        _audioSource.pitch = Random.Range(-2.0f, 2.0f);
        _audioSource.volume = Random.Range(0.0f, 0.5f);
        _originalScale = transform.localScale;
        StartCoroutine(ScaleUp());
    }

    IEnumerator ScaleUp()
    {
        transform.localScale = Vector3.zero;
        float timeElapsed = 0.0f;
        while (timeElapsed < scaleDuration)
        {
            float scaleFactor = timeElapsed / scaleDuration * maxScale;
            Vector3 newScale = _originalScale * scaleFactor;
            transform.localScale = newScale;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = _originalScale;
    }


    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (!Globals.InBounds(transform.position))
        {
            DestroyObject();
        }
    }

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;

            if (gameObject.GetComponent<HouseForBees>() != null)
            {
                Globals.GameOutcome = GameOutcome.Loss;
                Health = Globals.MaxHiveHealth;
                SceneManager.LoadScene("Menu");
            }
            else if (gameObject.GetComponent<Boss>() != null)
            {
                Globals.GameOutcome = GameOutcome.Win;
                SceneManager.LoadScene("Menu");
            }
            else
                DestroyObject();
        }
    }

    protected virtual void DestroyObject()
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowName();
    }

    public virtual void ShowName()
    {
        OnNotify?.Invoke(gameObject, gameObject.name);
    }
}