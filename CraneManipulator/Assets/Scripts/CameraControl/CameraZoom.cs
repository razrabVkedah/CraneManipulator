using UnityEngine;

namespace CameraControl
{
    public class CameraZoom : MonoBehaviour
    {
        [Header("Draw distances")]
        [SerializeField] private float farClipPlane = 50f;
        [SerializeField] private float nearClipPlane = 0.01f;
        [Header("Zoom")]
        [SerializeField] private Camera cameraForZoom;
        [SerializeField] private float minZoom = 20f, maxZoom = 100f;
        [SerializeField] private float sensitive = -1000f;

        private void Start()
        {
            cameraForZoom.farClipPlane = farClipPlane;
            cameraForZoom.nearClipPlane = nearClipPlane;
        }

        private void Update()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if(scroll == 0) return;
            cameraForZoom.fieldOfView = Mathf.Clamp(cameraForZoom.fieldOfView + scroll * sensitive * Time.deltaTime,
                minZoom, maxZoom);
        }
    }
}
