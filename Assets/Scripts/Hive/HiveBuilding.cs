using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class HiveBuilding : MonoBehaviour
{
    private GameObject _selectedImage;
    private GameObject _unselectedImage;

    private GameObject _information;
    public bool selected;

    public static Action<GameObject> OnSelected;
    public static Action OnUnSelected;
    
    private void Start()
    {
        _selectedImage = transform.GetChild(0).gameObject;
        _unselectedImage = transform.GetChild(1).gameObject;
        Unselect();
    }

    public void Select()
    {
        _selectedImage.gameObject.SetActive(true);
        _unselectedImage.gameObject.SetActive(false);
        selected = true;
    }

    public void Unselect()
    {
        _selectedImage.gameObject.SetActive(false);
        _unselectedImage.gameObject.SetActive(true);
        selected = false;
    }

    private void OnMouseDown()
    {
        if (!selected)
        {
            OnSelected.Invoke(gameObject);
            selected = true;
        }
        else
        {
            OnUnSelected?.Invoke();
            selected = false;
        }
    }

    public abstract string GetName();

    public abstract string GetDescription();

    public abstract Sprite GetImage();
}