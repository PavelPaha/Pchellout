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
    private Building _placedBuilding;
    private Vector2 _lastPlacementPosition;

    void Start()
    {
        // for now, we'll automatically pick our first
        // building type as the type we want to build
        _PreparePlacedBuilding(0);
    }

    void Update()
    {
        if (_placedBuilding == null)
            return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _CancelPlacedBuilding();
            return;
        }

        if (Input.GetMouseButtonDown(0))
            _PlaceBuilding();
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            _PlaceBuilding();
    }

    void _PreparePlacedBuilding(int buildingIndex)
    {
        var building = new Building(Globals.Buildings[buildingIndex]);
        // link the data into the manager
        _placedBuilding = building;
        _lastPlacementPosition = Vector2.zero;
    }

    void _PlaceBuilding()
    {
        _placedBuilding.Place();
        // keep on building the same building type
        _PreparePlacedBuilding(_placedBuilding.BuildingIndex);
    }

    void _CancelPlacedBuilding()
    {
        // destroy the "phantom" building
        Destroy(_placedBuilding.BuildingObject);
        _placedBuilding = null;
    }

    public void SelectPlacedBuilding(int buildingIndex) => _PreparePlacedBuilding(buildingIndex);
}