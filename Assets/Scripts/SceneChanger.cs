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
        SceneManager.LoadScene(TargetSceneName);
    }
}

