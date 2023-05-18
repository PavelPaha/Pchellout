using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int timeToExplode;
    public GameObject explosion;
    private float _timeSinceInitialization;
    private float _initializationTime;

    void Start()
    {
        _initializationTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        _timeSinceInitialization = Time.timeSinceLevelLoad - _initializationTime;
        if (_timeSinceInitialization > timeToExplode)
        {
            var currentTransform = transform;
            Instantiate(explosion, currentTransform.position, currentTransform.rotation, currentTransform.parent);
            Destroy(gameObject);
        }
    }
}