using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //private BuildingPlacer _buildingPlacer;

    public Transform buildingMenu;
    public GameObject buildingButtonPrefab;

    private void Awake()
    {
        //_buildingPlacer = GetComponent<BuildingPlacer>();

        // create buttons for each building type
        for (var i = 0; i < Globals.Buildings.Length; i++)
        {
            // var button = Instantiate(
            //     buildingButtonPrefab,
            //     buildingMenu);
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
        //b.onClick.AddListener(() => _buildingPlacer.SelectPlacedBuilding(i));
    }
}