using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BasicBee))]
public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private GameObject healthBar;
    private BasicBee _basicBee;
    private float _maxHealth;

    void Awake()
    {
        if (!gameObject.CompareTag("Hive"))
            healthBar.SetActive(false);
        _basicBee = GetComponent<BasicBee>();
        _maxHealth = _basicBee.Health;
    }

    void Update()
    {
        bar.fillAmount = _basicBee.Health / _maxHealth;
        if (!gameObject.CompareTag("Hive"))
            healthBar.SetActive(Input.GetKey(KeyCode.LeftAlt));
    }
}