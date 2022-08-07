using InputUI;
using UIManagers;
using UnityEngine;

namespace CameraControl
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private Transform cameraPivot;
        [SerializeField] private UIButton screen;
        [Header("Clamp vertical euler angles")]
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        
        private float _verticalRotation;
        private Vector2 _touchFirstPoint;
        
        private void Start()
        {
            var rotY = cameraPivot.localEulerAngles.y;
            
            cameraPivot.localEulerAngles = new Vector3(-_verticalRotation, rotY, 0f);
        }

        private void Update()
        {
            if (screen.IsPressed() == false) return;

            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if(touch.phase != TouchPhase.Moved) return;
                
                if(touch.deltaPosition == Vector2.zero) return;
                
                var delta = touch.deltaPosition.normalized;
                
                _verticalRotation -= delta.y * MenuUI.SensitivityY * Time.deltaTime;
                _verticalRotation = Mathf.Clamp(_verticalRotation, minX, maxX);
                var rotY = cameraPivot.localEulerAngles.y -
                           delta.x * MenuUI.SensitivityX * Time.deltaTime;

                cameraPivot.localEulerAngles = new Vector3(-_verticalRotation, rotY, 0f);
            }
            
            else if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                _verticalRotation += Input.GetAxis("Mouse Y") * MenuUI.SensitivityY * Time.deltaTime;
                _verticalRotation = Mathf.Clamp(_verticalRotation, minX, maxX);
                var rotY = cameraPivot.localEulerAngles.y +
                           Input.GetAxis("Mouse X") * MenuUI.SensitivityX * Time.deltaTime;

                cameraPivot.localEulerAngles = new Vector3(-_verticalRotation, rotY, 0f);
            }
        }
    }
}
