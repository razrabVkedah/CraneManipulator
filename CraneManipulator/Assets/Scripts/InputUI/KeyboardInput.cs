using UnityEngine;
using UnityEngine.Events;

namespace InputUI
{
    public class KeyboardInput : MonoBehaviour
    {
        [Header("On press Space")]
        public UnityEvent onClickSpaceButton = new();
        [Space]
        [Header("On click C button")]
        public UnityEvent onClickCButton = new();
        [Space]
        [Header("On click Esc button")]
        public UnityEvent onClickEscapeButton = new();

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) onClickSpaceButton.Invoke();
            
            if(Input.GetKeyDown(KeyCode.C)) onClickCButton.Invoke();
            
            if(Input.GetKeyDown(KeyCode.Escape)) onClickEscapeButton.Invoke();
        }
    }
}
