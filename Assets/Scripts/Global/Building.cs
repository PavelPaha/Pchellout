using System.Linq;
using Global;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class Building
{
    public readonly GameObject BuildingObject;
    public readonly string Name;
    public readonly int MaxHp;
    public readonly int Cost;

    private BuildingPlacement _placement;

    public int Hp { get; }
    public bool IsFixed => _placement == BuildingPlacement.Fixed;

    public int BuildingIndex => Enumerable
        .Range(0, Globals.Buildings[Globals.CameraIsInHive ? "hive" : "world"].Length)
        .First(i => Globals.Buildings[Globals.CameraIsInHive ? "hive" : "world"][i].Name == Name);

    public Building(BuildingData data)
    {
        Name = data.Name;
        MaxHp = data.Hp;
        Hp = MaxHp;
        Cost = data.Cost["honey"];
        _placement = BuildingPlacement.Valid;

        var parent = GameObject.Find(Globals.CameraIsInHive ? "HiveBuildings" : "Flowers");
        if (data.Name != "Bomb")
            BuildingObject = Object.Instantiate(Resources.Load<GameObject>($"Buildings/{data.Name}"));
        else
            BuildingObject = Object.Instantiate(Resources.Load<GameObject>($"Buildings/{data.Name}"));
    }


    public void SetPosition(Vector2 position) => BuildingObject.transform.position = position;
}