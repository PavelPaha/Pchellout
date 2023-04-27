using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBee : MonoBehaviour
{
    public int Health = 100;
    private HealthBarController healthBarController;
    void Start()
    {
        healthBarController = GetComponent<HealthBarController>();
        healthBarController.Health = Health;
    }

    void OnMouseDown()
    {
        Health -= 20;
        healthBarController.DamageHealth(20);
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
}
