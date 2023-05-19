using UnityEngine;
using UnityEngine.UI;

public class TimerBarController : MonoBehaviour
{
    public float timeToExplode;
    public Image timerBar;

    public float TimeSinceInitialization { get; private set; }

    private float _initializationTime;
    private float _currentTimeToExplode;

    void Start()
    {
        _initializationTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        TimeSinceInitialization = Time.timeSinceLevelLoad - _initializationTime;
        _currentTimeToExplode = timeToExplode - TimeSinceInitialization;
        var percent = _currentTimeToExplode / timeToExplode;
        timerBar.fillAmount = percent;
    }
}