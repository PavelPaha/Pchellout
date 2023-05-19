using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;

    private TimerBarController _timerBarController;

    void Start()
    {
        _timerBarController = gameObject.GetComponent<TimerBarController>();
    }

    void Update()
    {
        if (_timerBarController.TimeSinceInitialization > _timerBarController.timeToExplode)
        {
            var currentTransform = transform;
            Instantiate(explosion, currentTransform.position, currentTransform.rotation, currentTransform.parent);
            Destroy(gameObject);
        }
    }
}