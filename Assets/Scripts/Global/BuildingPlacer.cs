using UnityEngine;
using UnityEngine.EventSystems;

public enum BuildingPlacement
{
    Valid,
    Invalid,
    Fixed
};

public class BuildingPlacer : MonoBehaviour
{
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

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            PlaceBuilding();
    }


    public void PreparePlacedBuilding(int buildingIndex)
    {
        var building = new Building(Globals.Buildings[buildingIndex]);
        _placedBuilding = building;
        FreezeBuilding(true);
        UpdateBuildingPosition();
    }

    public void PlaceBuilding()
    {
        FreezeBuilding(false);
        PreparePlacedBuilding(_placedBuilding.BuildingIndex);
    }

    public void CancelPlacedBuilding()
    {
        Destroy(_placedBuilding.BuildingObject);
        _placedBuilding = null;
    }

    public void SelectPlacedBuilding(int buildingIndex) => PreparePlacedBuilding(buildingIndex);

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
    }
}