using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnBee : MonoBehaviour
{
    public GameObject Bee;
    public GameObject BeesSource;
    public GameObject HiveInstance;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("Пиждор");
        Vector3 hivePosition = HiveInstance.transform.position;
        var defender = Instantiate(Bee, hivePosition, Quaternion.identity);
        defender.GetComponent<DefaultNamespace.BeeEnemy>().BeesSource = BeesSource;
    }
}
