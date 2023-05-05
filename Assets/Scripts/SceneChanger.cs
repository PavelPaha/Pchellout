using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string TargetSceneName;
    void Start()
    {
        
    }
    
    private void OnMouseDown()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        SceneManager.LoadScene(TargetSceneName, LoadSceneMode.Additive);
    }
}

