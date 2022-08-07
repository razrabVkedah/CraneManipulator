using UnityEngine;
using UnityEngine.EventSystems;

namespace InputUI
{
    public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float pressedValue, unpressedValue;

        public float GetButtonValue() => _isPressed == true ? pressedValue : unpressedValue;
        
        public bool IsPressed() => _isPressed;
        
        private bool _isPressed;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }
    }
}
