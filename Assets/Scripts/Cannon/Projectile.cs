using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= 3)
            Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Enemy":
                col.gameObject.GetComponent<BeeEnemy>().Damage(Globals.ProjectileDamage);
                Destroy(gameObject);
                break;
            case "Boss":
                col.gameObject.GetComponent<Boss>().Damage(Globals.ProjectileDamage);
                Destroy(gameObject);
                break;
        }
    }
}
