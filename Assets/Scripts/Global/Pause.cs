using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Canvas pauseCanvas;

        private float _timer;

        private const float DelayTime = 1f;

        public void Start()
        {
            pauseCanvas.enabled = false;
            pauseButton.onClick.AddListener(() => { DoPause(); });

            continueButton.onClick.AddListener(() => { DoContinue(); });

            exitButton.onClick.AddListener(() =>
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
            pauseCanvas.enabled = false;
            Time.timeScale = 1;
        }

        public void Update()
        {
            _timer += Time.deltaTime;
            if (Input.GetKey(KeyCode.Escape) && _timer >= DelayTime)
            {
                _timer = 0;
                Debug.Log($"Pause = {Globals.Pause}");
                if (Globals.Pause)
                {
                    Debug.Log("YES");
                    DoContinue();
                    Debug.Log("PIDAR");
                }
                else
                    DoPause();
            }
        }

        private void DoPause()
        {
            Globals.Pause = true;
            pauseCanvas.enabled = true;
            Time.timeScale = 0;
        }
    }
}