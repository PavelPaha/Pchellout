using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class Building
{
    public readonly GameObject BuildingObject;
    public readonly string Name;
    public readonly int Cost;

    public int BuildingIndex => Enumerable
        .Range(0, Globals.Buildings[Globals.CameraIsInHive ? "hive" : "world"].Length)
        .First(i => Globals.Buildings[Globals.CameraIsInHive ? "hive" : "world"][i].Name == Name);

    public Building(BuildingData data)
    {
        Name = data.Name;
        Cost = data.Cost["honey"];
        var flowersParent = GameObject.Find("Flowers");
        BuildingObject = Object.Instantiate(Resources.Load<GameObject>($"Buildings/{data.Name}"));
        if (data.Name != "Бомба")
            BuildingObject.transform.SetParent(flowersParent.transform);
    }


    public void SetPosition(Vector2 position) => BuildingObject.transform.position = position;
}