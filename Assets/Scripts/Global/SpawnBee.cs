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
        if (Globals.GameResources["honey"].Amount - Globals.DefenderPrice >= 0)
        {
            Vector3 hivePosition = Hive.transform.position;
            var bee = Instantiate(Bee, hivePosition, Quaternion.identity);
            bee.transform.localScale = new Vector3(Globals.DefenderScale, Globals.DefenderScale, Globals.DefenderScale);
            bee.GetComponent<BeeDefender>().BeesSource = Goal;
            bee.transform.SetParent(ParentForDefenders.transform);
            Globals.GameResources["honey"].Amount -= Globals.DefenderPrice;
            OnBuy?.Invoke();
        }
        
    }
    
    private void UpdatePressPossibility()
    {
        Globals.UpdateBuyPossibility(gameObject.GetComponent<Button>(), Globals.ExtractorPrice);
    }
}
