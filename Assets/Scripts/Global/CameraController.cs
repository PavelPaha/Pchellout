using Unity.VisualScripting;
using UnityEngine;

namespace Global
{
    public class CameraController : MonoBehaviour
    {
        public float targetAspectRatio = 16f / 9f;
        private Camera _camera;

        public void Start()
        {
            _camera = GetComponent<Camera>();
            var windowAspect = (float)Screen.width / (float)Screen.height;
            var scaleHeight = windowAspect / targetAspectRatio;
            if (scaleHeight < 1.0f)
            {
                Rect rect = _camera.rect;
                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;
                _camera.rect = rect;
            }
            else
            {
                float scaleWidth = 1.0f / scaleHeight;
                Rect rect = _camera.rect;
                rect.width = scaleWidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scaleWidth) / 2.0f;
                rect.y = 0;
                _camera.rect = rect;
            }
        }
    }

}