using UnityEngine;

[RequireComponent(typeof(BasicBee))]
public class HealthUpper : MonoBehaviour
{
    public int healthBoostCost = 500;
    private BasicBee _basicBee;
    private float _maxHealth;

    void Awake()
    {
        _basicBee = GetComponent<BasicBee>();
        _maxHealth = _basicBee.Health;
    }

    void OnMouseDown()
    {
        if (Globals.GameResources["honey"].Amount < healthBoostCost || _basicBee.Health > _maxHealth / 2)
            return;
        _basicBee.Health = (int)_maxHealth;
        Globals.GameResources["honey"].Amount -= healthBoostCost;
    }
}