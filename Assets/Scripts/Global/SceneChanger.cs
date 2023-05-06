using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Start()
    {
        
    }
    
    private void OnMouseDown()
    {
        if (Globals.CameraIsInHive)
        {
            Camera.main.transform.Translate(new Vector3(-45, 0, 0));
        }
        else
        {
            Camera.main.transform.Translate(new Vector3(45, 0, 0));
        }

        Globals.CameraIsInHive = !Globals.CameraIsInHive;
    }
}

