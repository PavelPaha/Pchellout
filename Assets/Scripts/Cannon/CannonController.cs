using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed = 10f;
    public float FireCooldown = 0.1f; // Минимальное время между выстрелами
    private float fireTimer = 0f;
    
    
    void Update()
    {
        fireTimer += Time.deltaTime;

        // Получаем экранные координаты курсора и переводим их в мировые координаты
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Поворачиваем пушку так, чтобы она смотрела на позицию курсора
        transform.LookAt(mousePos, Vector3.forward);
        // Отменяем поворот пушки по оси Z
        transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
        
        if (Input.GetMouseButton(0) && fireTimer >= FireCooldown)
        {
            Debug.Log("Выстрел");
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

            // Направляем снаряд в сторону курсора с заданной скоростью
            Vector2 shootDirection = (mousePos - transform.position).normalized;
            projectileRigidbody.AddForce(shootDirection * ProjectileSpeed, ForceMode2D.Impulse);
        
            fireTimer = 0f;
        }
    }
}
