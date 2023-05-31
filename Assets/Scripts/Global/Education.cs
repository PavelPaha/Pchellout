

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Education : MonoBehaviour
{
    public Button Continue;
    public VideoPlayer Player;
    void Start()
    {
        Continue.onClick.AddListener(() => { SceneManager.LoadScene("Something"); });
        Player.loopPointReached += (VideoPlayer vp) => { SceneManager.LoadScene("Something"); };
    }
}
