using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform buildingMenu;
    public GameObject buildingButtonPrefab;
    private BuildingPlacer _buildingPlacer;
    
    private List<GameObject> _createdButtons;
 
    private void Awake()
    {
        _createdButtons = new List<GameObject>();
        _buildingPlacer = GetComponent<BuildingPlacer>();
        SceneChanger.OnChangeScene += ShowBuildingButtons;
        ShowBuildingButtons("world");
    }

    public void ShowBuildingButtons(string location)
    {
        foreach (var button in _createdButtons)
        {
            Destroy(button);
        }
        
        // location = world | hive
        for (var i = 0; i < Globals.Buildings[location].Length; i++)
        {
            var button = Instantiate(buildingButtonPrefab, buildingMenu);
            var text = Globals.Buildings[location][i].Name;
            button.name = text;
            button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = text;
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                Globals.Buildings[location][i].Cost["honey"].ToString();
            
            _createdButtons.Add(button);
            var buttonComponent = button.GetComponent<Button>();
            _AddBuildingButtonListener(buttonComponent, i);
        }
    }
    
    private void _AddBuildingButtonListener(Button buttonComponent, int i) =>
        buttonComponent.onClick.AddListener(() =>
        {
            if (_buildingPlacer.IsBuildingSelected)
                _buildingPlacer.CancelPlacedBuilding();
            _buildingPlacer.SelectPlacedBuilding(i);
        });
}