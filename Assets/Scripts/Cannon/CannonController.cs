using System;
using Global;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    private float fireTimer;
    private Animator _animator;
    private static readonly int IsFire = Animator.StringToHash("is_fire");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        // Получаем экранные координаты курсора и переводим их в мировые координаты
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Поворачиваем пушку так, чтобы она смотрела на позицию курсора
        transform.LookAt(mousePos, Vector3.forward);
        // Отменяем поворот пушки по оси Z
        transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
        if (Input.GetMouseButton(0))
        {
            _animator.SetBool(IsFire, true);
        }
        else
        {
            _animator.SetBool(IsFire, false);
        }
        
        if (Input.GetMouseButton(0) 
            && fireTimer >= Globals.FireCooldown
            && Globals.InBounds(mousePos))
        {
            // Debug.Log("Выстрел");
            if (Globals.GameResources["honey"].Amount >= Globals.ShotCost)
            {
                Fire(mousePos);
                Globals.GameResources["honey"].Amount -= Globals.ShotCost;
            }
            else
            {
                //TODO показывать сообщение, что мёда на выстрел недостаточно
            }
            fireTimer = 0f;
        }
    }

    private void Fire(Vector3 mousePos)
    {
        GameObject projectile =
            Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        var trail = projectile.GetComponent<TrailRenderer>();
        trail.time = 2 * Globals.ProjectileDamage / Globals.MaxProjectileDamage;
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        // Направляем снаряд в сторону курсора с заданной скоростью
        Vector2 shootDirection = (mousePos - transform.position);
        shootDirection.Normalize();
        projectileRigidbody.AddForce(shootDirection * Globals.ProjectileSpeed, ForceMode2D.Impulse);

    }
}
