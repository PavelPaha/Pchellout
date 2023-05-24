using System;
using DefaultNamespace;
using Global;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBee : MonoBehaviour
{
    public GameObject ParentForDefenders;
    public GameObject Bee;
    public GameObject Hive;
    public GameObject Goal;
    public static Action OnBuy;

    public void Start()
    {
        ResourcesUpdater.OnUpdated += UpdatePressPossibility;
    }

    public void OnMouseDown()
    {
        if (Hive == null) return;
        if (Globals.GameResources["honey"].Amount - Globals.DefenderCost >= 0)
        {
            Vector3 hivePosition = Hive.transform.position;
            var bee = Instantiate(Bee, hivePosition, Quaternion.identity);
            bee.GetComponent<BeeDefender>().BeesSource = Goal;
            bee.transform.SetParent(ParentForDefenders.transform);
            Globals.GameResources["honey"].Amount -= Globals.DefenderCost;
            OnBuy?.Invoke();
        }
        
    }
    
    private void UpdatePressPossibility()
    {
        Globals.UpdateBuyPossibility(gameObject.GetComponent<Button>(), Globals.ExtractorPrice);
    }
}
