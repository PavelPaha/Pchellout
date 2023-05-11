using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class Pause : MonoBehaviour
    {
        public Button PauseButton;
        public Button ContinueButton;
        public Button ExitButton;

        public Canvas PauseCanvas;

        public void Start()
        {
            PauseCanvas.enabled = false;
            PauseButton.onClick.AddListener(() =>
            {
                Globals.Pause = true;
                PauseCanvas.enabled = true;
                Time.timeScale = 0;
            });
            
            ContinueButton.onClick.AddListener(() =>
            {
                Globals.Pause = true;
                PauseCanvas.enabled = false;
                Time.timeScale = 1;
            });

            ExitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
        }
    }
}