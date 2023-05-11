using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class Building
{
    public readonly GameObject BuildingObject;
    public readonly string Name;
    public readonly int MaxHp;

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
        _placement = BuildingPlacement.Valid;

        var parent = GameObject.Find(Globals.CameraIsInHive ? "HiveBuildings" : "Flowers");
        var buildingObject = Object.Instantiate(Resources.Load<GameObject>($"Buildings/{data.Name}"), 
            parent.transform);
        BuildingObject = buildingObject;
    }

    public void SetPosition(Vector2 position) => BuildingObject.transform.position = position;
}