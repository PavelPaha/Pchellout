using System.Linq;
using Hive;
using TMPro;
using UnityEngine;

public class ResourceMapper : MonoBehaviour
{
    public GameObject HoneyTextMeshObject;
    public GameObject HiveObjects;

    private TextMeshProUGUI _honeyTextMesh;
    private TextMeshProUGUI _pollenTextMesh;

    void Start()
    {
        _honeyTextMesh = HoneyTextMeshObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var actualHoneyCount = Globals.GameResources["honey"].Amount;
        for (var i = 0; i < HiveObjects.transform.childCount; i++)
        {
            var current = HiveObjects.transform.GetChild(i).GetComponent<HiveBuilding>();
            if (current is IHoneyContainer)
                ((IHoneyContainer)current).Honey = actualHoneyCount;
        }

        _honeyTextMesh.text = actualHoneyCount.ToString();
    }

    private int GetObjectsCount<T>() => Enumerable
        .Range(0, HiveObjects.transform.childCount)
        .Select(i => HiveObjects.transform.GetChild(i).GetComponent<HiveBuilding>())
        .Count(obj => obj is T);
}