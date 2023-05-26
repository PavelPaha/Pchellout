using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    public static Action OnExploded;

    private AudioSource _audioSource;
    private TimerBarController _timerBarController;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _timerBarController = gameObject.GetComponent<TimerBarController>();
    }

    void Update()
    {
        if (_timerBarController.TimeSinceInitialization > _timerBarController.timeToExplode)
        {
            var currentTransform = transform;
            Instantiate(explosion, currentTransform.position, currentTransform.rotation, currentTransform.parent);
            OnExploded?.Invoke();
            _audioSource.Play();
            Destroy(gameObject);
        }
    }
}