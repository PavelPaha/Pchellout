using System.Collections;
using System.Collections.Generic;
using Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notifier : MonoBehaviour
{
    public Canvas ParentCanvas;
    public GameObject infoPanelPrefab;
    private GameObject infoPanel;

    void Start()
    {
        BasicBee.OnNotify += Notify;
        HiveMenuInformationUpdater.OnNotify += Notify;
        infoPanel = infoPanelPrefab;
        infoPanel.transform.SetParent(ParentCanvas.transform);
        infoPanel.SetActive(false);
    }

    public void Notify(GameObject source, string message)
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = message;
        Vector2 mousePos = Input.mousePosition;

        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        var rectTransform = infoPanel.GetComponent<RectTransform>();
        rectTransform.position = new Vector3(worldPos.x, worldPos.y, 0f);
        StartCoroutine(HidePanel(source));
    }

    IEnumerator HidePanel(GameObject source)
    {
        var elapsedTime = 0f;
        while (elapsedTime < 1)
        {
            infoPanel.transform.position = source.transform.position + new Vector3(0, 0.5f, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        infoPanel.SetActive(false);
    }
}