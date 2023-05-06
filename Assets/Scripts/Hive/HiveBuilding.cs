using UnityEngine;

public abstract class HiveBuilding : MonoBehaviour
{
    private GameObject SelectedImage;
    private GameObject UnselectedImage;

    private GameObject Information;
    public bool Selected;

    private void Start()
    {
        SelectedImage = transform.GetChild(0).gameObject;
        UnselectedImage = transform.GetChild(1).gameObject;
        Unselect();
    }

    public void Select()
    {
        SelectedImage.gameObject.SetActive(true);
        UnselectedImage.gameObject.SetActive(false);
        Selected = true;
    }

    public void Unselect()
    {
        SelectedImage.gameObject.SetActive(false);
        UnselectedImage.gameObject.SetActive(true);
        Selected = false;
    }

    private void OnMouseDown()
    {
        if (!Selected)
        {
            Select();
            Selected = true;
        }
        else
        {
            Unselect();
            Selected = false;
        }
    }

    public abstract string GetName();

    public abstract string GetDescription();

    public abstract Sprite GetImage();
}