using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform buildingMenu;
    public GameObject buildingButtonPrefab;
    private BuildingPlacer _buildingPlacer;

    private void Awake()
    {
        _buildingPlacer = GetComponent<BuildingPlacer>();
        for (var i = 0; i < Globals.Buildings.Length; i++)
        {
            var button = Instantiate(buildingButtonPrefab, buildingMenu);
            var text = Globals.Buildings[i].Name;
            button.name = text;
            button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = text;
            var buttonComponent = button.GetComponent<Button>();
            _AddBuildingButtonListener(buttonComponent, i);
        }
    }

    private void _AddBuildingButtonListener(Button b, int i)
    {
        b.onClick.AddListener(() => _buildingPlacer.SelectPlacedBuilding(i));
    }
}