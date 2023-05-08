using DefaultNamespace;
using UnityEngine;

public class SpawnBee : MonoBehaviour
{
    public GameObject ParentForDefenders;
    public GameObject Bee;
    public GameObject Hive;
    public GameObject Goal;

    public void OnMouseDown()
    {
        if (Hive == null) return;
        Vector3 hivePosition = Hive.transform.position;
        var bee = Instantiate(Bee, hivePosition, Quaternion.identity);
        bee.GetComponent<BeeDefender>().BeesSource = Goal;
        bee.transform.SetParent(ParentForDefenders.transform);
    }
}
