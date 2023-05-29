using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas MenuCanvas;
    public Canvas GameOutcomeCanvas;
    public TextMeshProUGUI Description;
    
    public Button BeginButton;
    public Button ExitButton;

    public TextMeshProUGUI LossScore;
    
    
    void Start()
    {

        BeginButton.onClick.AddListener(BeginGame);
        ExitButton.onClick.AddListener(ExitGame);

        GameOutcomeCanvas.enabled = false;
        MenuCanvas.enabled = true;
    }

    public void Update()
    {
        switch (Globals.GameOutcome)
        {
            case GameOutcome.Loss:
                Globals.GameOutcome = GameOutcome.Menu;
                BeginButton.gameObject.SetActive(false);
                ShowLossWindow();
                break;
            case GameOutcome.Win:
                Globals.GameOutcome = GameOutcome.Menu;
                BeginButton.gameObject.SetActive(false);
                ShowWinWindow();
                break;
        }
    }


    private void ShowLossWindow()
    {
        GameOutcomeCanvas.enabled = true;
        Description.text = "Проигрыш(";
        LossScore.gameObject.SetActive(false);
    }
    
    private void ShowWinWindow()
    {
        GameOutcomeCanvas.enabled = true;
        LossScore.gameObject.SetActive(true);
        Description.text = "Поздравляем, вы победили босса";
        LossScore.text = Globals.GameResources["honey"].Amount.ToString();
    }
    

    private void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void BeginGame()
    {
        Globals.Reset();
        MenuCanvas.enabled = false;
        SceneManager.LoadScene("Something");
    }
}
