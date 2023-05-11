using System.Linq;
using Hive;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public HiveBuilding SelectedItem;
    public GameObject InformationWindow;
    public GameObject TextMeshItem;
    public Image Picture;

    public GameObject HoneyTextMeshObject;

    private TextMeshProUGUI _honeyCount;
    private TextMeshProUGUI _textMesh;
    
    private void Start()
    {
        SceneChanger.OnChangeScene += HideSelectedBuilding;
        SelectedItem = null;
        _textMesh = TextMeshItem.GetComponent<TextMeshProUGUI>();
        _honeyCount = HoneyTextMeshObject.GetComponent<TextMeshProUGUI>();
        // InformationWindow = Menu.transform.GetChild(1).GetComponent<GameObject>();
        InformationWindow.SetActive(false);
        HoneyTextMeshObject.SetActive(false);
        HiveBuilding.OnSelected += Show;
        HiveBuilding.OnUnSelected += Hide;
    }

    public void Show(GameObject obj)
    {
        obj.GetComponent<HiveBuilding>().Select();
        Debug.Log($"ObjectSelector: Мне попал в руки {obj.name}");
        SelectedItem?.Unselect();
        SelectedItem = obj.GetComponent<HiveBuilding>();
        ShowMenu();
    }

    public void HideSelectedBuilding(string location)
    {
        if (location == "world")
        {
            Hide();
        }
    }
    
    public void Hide()
    {
        Debug.Log($"Говорит ObjectSelector: отмена выбора элемента");
        SelectedItem?.Unselect();
        SelectedItem = null;
        HideMenu();
    }

    public void HideMenu()
    {
        InformationWindow.SetActive(false);
        HoneyTextMeshObject.SetActive(false);
    }
    public void ShowMenu()
    {
        InformationWindow.SetActive(true);
        var name = SelectedItem.transform.name;
        var description = SelectedItem.GetDescription();
        Picture.sprite = SelectedItem.GetImage();
        _textMesh.text = $"{name}\n\n{description}";

        if (SelectedItem is IHoneyContainer)
        {
            HoneyTextMeshObject.SetActive(true);
            var asHoney = ((IHoneyContainer)SelectedItem);
            _honeyCount.text = asHoney.Honey.ToString();
        }
        else
        {
            HoneyTextMeshObject.SetActive(false);
        }
    }
}