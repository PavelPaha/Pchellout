using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovement : MonoBehaviour
{
    public float speed = 15.0f;
    public float edgeMargin = 20.0f;
    public float smoothTime = 0.3f;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 newPosition = mainCamera.transform.position;
        float horizontalSpeed = 0;
        float verticalSpeed = 0;

        if (Input.mousePosition.x > 0 && Input.mousePosition.x < edgeMargin)
        {
            horizontalSpeed = -speed;
        }
        else if (Input.mousePosition.x < Screen.width && Input.mousePosition.x > Screen.width - edgeMargin)
        {
            horizontalSpeed = speed;
        }

        if (Input.mousePosition.y > 0 && Input.mousePosition.y < edgeMargin)
        {
            verticalSpeed = -speed;
        }
        else if (Input.mousePosition.y < Screen.height && Input.mousePosition.y > Screen.height - edgeMargin)
        {
            verticalSpeed = speed;
        }

        newPosition.x += horizontalSpeed * Time.deltaTime;
        newPosition.y += verticalSpeed * Time.deltaTime;

        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, newPosition, ref velocity, smoothTime);
    }
}