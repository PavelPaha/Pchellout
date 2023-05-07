using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building
{
    public readonly Transform Transform;
    public readonly string Name;
    public readonly int MaxHp;
    
    private BuildingPlacement _placement;
    private List<Material> _materials;
    
    public int Hp { get; set; }

    public int BuildingIndex => Enumerable
        .Range(0, Globals.Buildings.Length)
        .First(i => Globals.Buildings[i].Name == Name);

    public Building(BuildingData data)
    {
        Name = data.Name;
        MaxHp = data.Hp;
        Hp = MaxHp;
        var instantiate = Object.Instantiate(
            Resources.Load($"Assets/Buildings/{data.Name}")
        ) as GameObject;
        Transform = instantiate.transform;
    }

    public void SetPosition(Vector2 position) => Transform.position = position;
}