using System.Linq;
using UnityEngine;

public class Building
{
    public readonly GameObject BuildingObject;
    public readonly string Name;
    public readonly int MaxHp;

    private BuildingPlacement _placement;

    public int Hp { get; set; }
    public bool IsFixed => _placement == BuildingPlacement.Fixed;

    public int BuildingIndex => Enumerable
        .Range(0, Globals.Buildings.Length)
        .First(i => Globals.Buildings[i].Name == Name);

    public Building(BuildingData data)
    {
        Name = data.Name;
        MaxHp = data.Hp;
        Hp = MaxHp;
        _placement = BuildingPlacement.Valid;
        var instantiate = Object.Instantiate(Resources.Load<GameObject>($"Flowers/{data.Name}"));
        BuildingObject = instantiate;
    }

    public void SetPosition(Vector2 position) => BuildingObject.transform.position = position;

    public void Place()
    {
        // set placement state
        _placement = BuildingPlacement.Fixed;
        // remove "is trigger" flag from box collider to allow
        // for collisions with units
        BuildingObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}