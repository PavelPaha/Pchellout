using System;
using System.Diagnostics;
using Global;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public enum BuildingPlacement
{
    Valid,
    Invalid,
    Fixed
};

public class BuildingPlacer : MonoBehaviour
{
    public static Action OnBuildingPlaced;
    public static Action OnShake;
    public static Action OnBuy;
    public bool IsBuildingSelected => _placedBuilding != null;

    private Building _placedBuilding;
    private float _buildingToCameraDistance;


    void Update()
    {
        if (_placedBuilding == null)
            return;
        
        UpdateBuildingPosition();
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CancelPlacedBuilding();
            return;
        }

        if ((InBounds() || _placedBuilding.Name == "Бомба")
            && Globals.GameResources["honey"].Amount >= _placedBuilding.Cost)
        {
            ChangeOpacity(1);
            if (Input.GetMouseButtonDown(0)
                && !EventSystem.current.IsPointerOverGameObject())
            {
                Globals.GameResources["honey"].Amount -= _placedBuilding.Cost;
                OnBuy?.Invoke();
                PlaceBuilding();
            }
        }
        else
        {
            ChangeOpacity(0.2f);
        }
    }

    private void ChangeOpacity(float value)
    {
        var currentColor = _placedBuilding.BuildingObject.GetComponent<SpriteRenderer>().color;
        _placedBuilding.BuildingObject.GetComponent<SpriteRenderer>().color = new Color(
            currentColor.r,
            currentColor.g,
            currentColor.b,
            value
        );
    }


    public void PreparePlacedBuilding(string location, int buildingIndex)
    {
        var building = new Building(Globals.Buildings[location][buildingIndex]);
        _placedBuilding = building;
        FreezeBuilding(true);
        UpdateBuildingPosition();
    }

    public void PlaceBuilding()
    {
        OnBuildingPlaced?.Invoke();
        if (_placedBuilding.Name != "Бомба")
        {
            OnShake?.Invoke();
            var parent = GameObject.Find("Flowers");
            _placedBuilding.BuildingObject.transform.SetParent(parent.transform);
        }

        FreezeBuilding(false);
        PreparePlacedBuilding("world", _placedBuilding.BuildingIndex);
    }

    public void CancelPlacedBuilding()
    {
        Destroy(_placedBuilding.BuildingObject);
        _placedBuilding = null;
    }

    public void SelectPlacedBuilding(int buildingIndex)
        => PreparePlacedBuilding("world", buildingIndex);

    private void UpdateBuildingPosition()
    {
        var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        _placedBuilding.SetPosition(curPosition);
    }

    private void FreezeBuilding(bool toFreeze)
    {
        foreach (var component in _placedBuilding.BuildingObject.GetComponents<Component>())
        {
            var behaviour = component as Behaviour;
            if (behaviour != null)
                behaviour.enabled = !toFreeze;
        }

        if (_placedBuilding.BuildingObject.TryGetComponent<Animator>(out var animator))
            animator.enabled = true;
    }

    private bool InBounds()
    {
        var x = Input.mousePosition.x;
        var y = Input.mousePosition.y;
        // Debug.Log($"{x} {y}");
        return x is >= 300 and <= 890 && y is >= 120 and <= 230;
    }
}