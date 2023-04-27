using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectState
{
    protected readonly ObjectSelector objectSelector;

    public ObjectState(ObjectSelector objectSelector)
    {
        this.objectSelector = objectSelector;
    }

    public virtual void OnMouseDown() { }
    public virtual void ShowInformation() { }
    public virtual void HideInformation() { }
    public virtual void SellObject() { }
}

public class NotSelectedState : ObjectState
{
    public NotSelectedState(ObjectSelector objectSelector) : base(objectSelector)
    {
    }

    public override void OnMouseDown()
    {
        objectSelector.SelectedImage.gameObject.SetActive(true);
        objectSelector.UnselectedImage.gameObject.SetActive(false);
        objectSelector.ButtonsCanvas.SetActive(true);
        objectSelector.SetState(new SelectedState(objectSelector));
    }
}

public class SelectedState : ObjectState
{
    public SelectedState(ObjectSelector objectSelector) : base(objectSelector)
    {
    }

    public override void OnMouseDown()
    {
        objectSelector.SelectedImage.gameObject.SetActive(false);
        objectSelector.UnselectedImage.gameObject.SetActive(true);
        objectSelector.ButtonsCanvas.SetActive(false);
        objectSelector.SetState(new NotSelectedState(objectSelector));
    }

    public override void ShowInformation()
    {
        objectSelector.Information.SetActive(true);
    }

    public override void HideInformation()
    {
        objectSelector.Information.SetActive(false);
    }

    public override void SellObject()
    {
        objectSelector.ButtonsCanvas.SetActive(false);
        Object.Destroy(objectSelector.gameObject);
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
            CurrentState.SellObject();
        });

        infoButton.onClick.AddListener(() =>
        {
            CurrentState.ShowInformation();
        });

        Information = ButtonsCanvas.transform.GetChild(1).gameObject;
        var hideButton = Information.transform.GetChild(0).GetComponent<Button>();

        hideButton.onClick.AddListener(() =>
        {
            CurrentState.HideInformation();
        });

        Information.SetActive(false);
        ButtonsCanvas.SetActive(false);
        SetState(new NotSelectedState(this));
    }

    public void SetState(ObjectState state)
    {
        CurrentState = state;
    }

    private void OnMouseDown()
    {
        CurrentState.OnMouseDown();
    }
}