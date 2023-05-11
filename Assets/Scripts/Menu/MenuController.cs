using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas MenuCanvas;
    public Canvas LossCanvas;
    
    public Button BeginButton;
    public Button ExitButton;

    public TextMeshProUGUI LossScore;
    
    
    void Start()
    {
        BeginButton.onClick.AddListener(BeginGame);
        ExitButton.onClick.AddListener(ExitGame);

        LossCanvas.enabled = false;
        MenuCanvas.enabled = true;
    }

    public void Update()
    {
        switch (Globals.GameOutcome)
        {
            case GameOutcome.Loss:
                Globals.GameOutcome = GameOutcome.Menu;
                ShowLossWindow();
                break;
            case GameOutcome.Default:
                Globals.GameOutcome = GameOutcome.Menu;
                //TODO если игра закончилась успешно
                break;
        }
    }


    private void ShowLossWindow()
    {
        LossCanvas.enabled = true;
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
        MenuCanvas.enabled = false;
        SceneManager.LoadScene("Something");
    }
}
