using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BasicBee))]
public class HealthBarController : MonoBehaviour
{
    public Image _bar;
    private BasicBee _basicBee;
    private float _maxHealth;

    void Awake()
    {
        _basicBee = GetComponent<BasicBee>();
        _maxHealth = _basicBee.Health;
    }

    void Update()
    {
        _bar.fillAmount = _basicBee.Health / _maxHealth;
    }
}