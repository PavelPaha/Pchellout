using UnityEngine;
using UnityEngine.UI;

public class SpawnBee : MonoBehaviour
{
    public GameObject Button;
    public GameObject Bee;
    public GameObject Hive;
    private GameObject hiveInstance;

    void Start()
    {
        hiveInstance = Instantiate(Hive);
    }

    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("Пиждор");
        Vector3 hivePosition = hiveInstance.transform.position;
        Instantiate(Bee, hivePosition, Quaternion.identity);
    }
}
