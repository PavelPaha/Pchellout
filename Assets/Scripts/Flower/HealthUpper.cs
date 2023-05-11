using UnityEngine;

[RequireComponent(typeof(BasicBee))]
public class HealthUpper : MonoBehaviour
{
    public int healthBoostCost = 20000;
    private BasicBee _basicBee;
    
    void Start() => _basicBee = gameObject.GetComponent<BasicBee>();

    void OnMouseDown()
    {
        if (Globals.GameResources["honey"].Amount < healthBoostCost || _basicBee.Health > 80)
            return;
        _basicBee.Health = 100;
        Globals.GameResources["honey"].Amount -= healthBoostCost;
    }
}
