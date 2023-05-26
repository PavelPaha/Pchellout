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

        private float timer = 0.0f;
        private float delayTime = 1f;

        public void Start()
        {
            PauseCanvas.enabled = false;
            PauseButton.onClick.AddListener(() => { DoPause(); });

            ContinueButton.onClick.AddListener(() => { DoContinue(); });

            ExitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
        }

        private void DoContinue()
        {
            Globals.Pause = false;
            PauseCanvas.enabled = false;
            Time.timeScale = 1;
        }

        public void Update()
        {
            timer += Time.deltaTime;
            Debug.Log($"{timer} {Globals.Pause}");
            if (Input.GetKey(KeyCode.Escape))
            {
                if (timer >= delayTime)
                {
                    timer = 0;
                    Debug.Log($"Pause = {Globals.Pause}");
                    if (Globals.Pause)
                    {
                        Debug.Log("YES");
                        DoContinue();
                        Debug.Log("PIDAR");
                    }
                    else DoPause();
                }
            }
            
        }

        private void DoPause()
        {
            Globals.Pause = true;
            PauseCanvas.enabled = true;
            Time.timeScale = 0;
        }
    }
}