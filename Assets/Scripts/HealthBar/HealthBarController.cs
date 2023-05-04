using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController: MonoBehaviour
{
    public Image _bar;
    public float Fill;
    private const int _maxHealth = 100;
    private BasicBee _basicBee; 

    private void Start()
    {
        Fill = 1f;
        _basicBee = GetComponent<BasicBee>();
    }

    void Update()
    {
        ChangeHealth(_basicBee.Health);
    }

    public void ChangeHealth(float actual)
    {
        Fill = actual / _maxHealth;
        _bar.fillAmount = Fill;
    }

    public void AddLife(float percent)
    {
        Fill = Math.Min(1, Fill + percent);
        _bar.fillAmount = Fill;
    }
}
