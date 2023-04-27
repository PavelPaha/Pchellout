using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public abstract class ObjectState
{
    protected readonly ObjectSelector objectSelector;

    public ObjectState(ObjectSelector objectSelector)
    {
        this.objectSelector = objectSelector;
    }

    public virtual void Do() { }
}

public class HideInformationState : ObjectState
{
    public HideInformationState(ObjectSelector objectSelector) : base(objectSelector)
    {
    }

    public override void Do()
    {
        objectSelector.Information.SetActive(false);
    }
}

public class ShowInformation : ObjectState
{
    public ShowInformation(ObjectSelector objectSelector) : base(objectSelector)
    {
    }

    public override void Do()
    {
        objectSelector.Information.SetActive(true);
    }
}

public class SelectState: ObjectState
{
    public SelectState(ObjectSelector objectSelector) : base(objectSelector)
    {
    }

    public override void Do()
    {
        var old = objectSelector.gameObject.transform.parent.gameObject.transform.GetComponent<Selector>()
            .SelectedObject;
        
        if (old.Equals(objectSelector.gameObject))
        {
            objectSelector.SelectedImage.gameObject.SetActive(!objectSelector.SelectedImage.gameObject.activeSelf);
            objectSelector.UnselectedImage.gameObject.SetActive(!objectSelector.UnselectedImage.gameObject.activeSelf);
            objectSelector.ButtonsCanvas.SetActive(!objectSelector.ButtonsCanvas.activeSelf);
        }
        else if (old != null && SameState(old))
        {
            var selector = old.gameObject.GetComponent<ObjectSelector>();
            selector.SelectedImage.gameObject.SetActive(false);
            selector.UnselectedImage.gameObject.SetActive(true);
            selector.ButtonsCanvas.SetActive(false);
            objectSelector.SelectedImage.gameObject.SetActive(true);
            objectSelector.UnselectedImage.gameObject.SetActive(false);
            objectSelector.ButtonsCanvas.SetActive(true);
        }
        objectSelector.gameObject.transform.parent.gameObject.transform.GetComponent<Selector>()
            .SelectedObject = objectSelector.gameObject;


    }

    private ObjectState ToggleState()
    {
        if (objectSelector.CurrentState is SelectState)
            return new UnselectState(objectSelector);
        return new SelectState(objectSelector);
    }

    private static bool SameState(GameObject old)
    {
        return old.gameObject.GetComponent<ObjectSelector>().CurrentState is SelectState;
    }
}

public class UnselectState: ObjectState
{
    public UnselectState(ObjectSelector objectSelector) : base(objectSelector)
    {
    }

    public override void Do()
    {
        objectSelector.SelectedImage.gameObject.SetActive(false);
        objectSelector.UnselectedImage.gameObject.SetActive(true);
        objectSelector.ButtonsCanvas.SetActive(false);
    }
}


public class SellState: ObjectState
{
    public SellState(ObjectSelector objectSelector) : base(objectSelector)
    {
    }

    public override void Do()
    {
        Object.Destroy(objectSelector.gameObject.transform.parent.gameObject.transform.GetComponent<Selector>()
            .SelectedObject.gameObject);
    }
}




public class ObjectSelector : MonoBehaviour
{
    public GameObject ButtonsCanvas;
    public GameObject Information;
    public GameObject SelectedImage;
    public GameObject UnselectedImage;

    public ObjectState CurrentState;
    
    private void Start()
    {
        SelectedImage = transform.GetChild(0).gameObject;
        UnselectedImage = transform.GetChild(1).gameObject;
        var sideBar = ButtonsCanvas.transform.GetChild(0);
        var sellButton = sideBar.GetChild(1).GetComponent<Button>();
        var infoButton = sideBar.GetChild(0).GetComponent<Button>();

        sellButton.onClick.AddListener(() =>
        {
            SetState(new SellState(this));
            CurrentState.Do();
        });
        

        infoButton.onClick.AddListener(() =>
        {
            SetState(new ShowInformation(this));
            CurrentState.Do();
        });

        Information = ButtonsCanvas.transform.GetChild(1).gameObject;
        var hideButton = Information.transform.GetChild(0).GetComponent<Button>();

        hideButton.onClick.AddListener(() =>
        {
            SetState(new HideInformationState(this));
            CurrentState.Do();
        });
        
        SelectedImage.SetActive(false);
        UnselectedImage.SetActive(true);
        Information.SetActive(false);
        ButtonsCanvas.SetActive(false);
        SetState(new SelectState(this));
    }

    public void SetState(ObjectState state)
    {
        CurrentState = state;
    }

    private void OnMouseDown()
    {
        SetState(new SelectState(this));
        CurrentState.Do();
    }
}