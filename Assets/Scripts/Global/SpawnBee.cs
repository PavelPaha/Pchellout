using DefaultNamespace;
using Mono.Cecil.Cil;
using UnityEngine;

public class SpawnBee : MonoBehaviour
{
    public GameObject ParentForDefenders;
    public GameObject Bee;
    public GameObject Hive;
    public GameObject Goal;

    public void OnMouseDown()
    {
        if (Globals.GameResources["honey"].Amount - Globals.DefenderCost >= 0)
        {
            if (Hive == null) return;
            Vector3 hivePosition = Hive.transform.position;
            Debug.Log("ABOBA");
            var bee = Instantiate(Bee, hivePosition, Quaternion.identity);
            bee.GetComponent<BeeDefender>().BeesSource = Goal;
            bee.transform.SetParent(ParentForDefenders.transform);
            Globals.GameResources["honey"].Amount -= Globals.DefenderCost;
        }
        
    }
}
