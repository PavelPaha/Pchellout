using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBarController: MonoBehaviour
{
    public Image _bar;
    public float Fill;
    private int _maxHealth = 100;
    private BasicBee _basicBee; 

    private void Start()
    {
        Fill = 1f;
        _basicBee = GetComponent<BasicBee>();
    }

    void Update()
    {
        try
        {
            ChangeHealth(_basicBee.Health);
        }
        catch
        {
            throw new Exception("HealthBarController не может получить доступ к здоровью объекта, чтобы обновить HealthBar");
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
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
