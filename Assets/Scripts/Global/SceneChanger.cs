using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static Action<string> OnChangeScene;
    
    private void OnMouseDown()
    {
        if (Globals.CameraIsInHive)
        {
            // Camera.main.transform.Translate(new Vector3(-45, 0, 0));
            OnChangeScene("world");
        }
        else
        {
            
            // Camera.main.transform.Translate(new Vector3(45, 0, 0));
            OnChangeScene("hive");
        }

        Globals.CameraIsInHive = !Globals.CameraIsInHive;
    }
}

