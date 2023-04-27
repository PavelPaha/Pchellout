using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController: MonoBehaviour
{
    public Image _bar;
    public int Health;
    public float Fill;

    private void Start()
    {
        Fill = 1f;
    }

    public void DamageHealth(float damage)
    {
        Fill -= damage / Health;
        _bar.fillAmount = Fill;
    }

    public void AddLife(float percent)
    {
        Fill = Math.Min(1, Fill + percent);
        _bar.fillAmount = Fill;
    }
}
