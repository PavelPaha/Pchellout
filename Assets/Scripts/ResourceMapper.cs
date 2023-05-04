using System.Collections;
using System.Collections.Generic;
using Hive;
using TMPro;
using UnityEngine;

public class ResourceMapper : MonoBehaviour
{
    public GameObject HoneyTextMeshObject;
    public GameObject PollenTextMeshObject;
    public GameObject HiveObjects;
    
    private TextMeshProUGUI _honeyTextMesh;
    private TextMeshProUGUI _pollenTextMesh;
    void Start()
    {
        _honeyTextMesh = HoneyTextMeshObject.GetComponent<TextMeshProUGUI>();
        _pollenTextMesh = PollenTextMeshObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var actualHoneyCount = 0;
        var actualPollenCount = 0;
        for (int i = 0; i < HiveObjects.transform.childCount; i++)
        {
            var current = HiveObjects.transform.GetChild(i).GetComponent<HiveObject>();
            if (current is IContainHoney)
                actualHoneyCount += ((IContainHoney)current).GetHoneyCount();

            if (current is IContainPollen)
                actualPollenCount += ((IContainPollen)current).GetPollenCount();
        }

        HiveResources.Honey = actualHoneyCount;
        HiveResources.Pollen = actualPollenCount;
        _honeyTextMesh.text = HiveResources.Honey.ToString();
        _pollenTextMesh.text = HiveResources.Pollen.ToString();
    }
}
