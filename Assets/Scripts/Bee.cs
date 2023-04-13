using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Bee : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Camera mainCamera;

    private SpriteRenderer _spriteRenderer;
    private Vector2 _previousPosition;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _previousPosition = transform.position;
    }

    private void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);

        transform.position += new UnityEngine.Vector3(0, 0, -transform.position.z-6);
        UpdateAnimationDirection();
        _previousPosition = transform.position;
    }

    private void UpdateAnimationDirection()
    {
        float movementDirection = ((Vector2)transform.position - _previousPosition).x;

        if (movementDirection > 0.01f)
        {
            _spriteRenderer.flipX = false;
        }
        else if (movementDirection < -0.01f)
        {
            _spriteRenderer.flipX = true;
        }
    }
}