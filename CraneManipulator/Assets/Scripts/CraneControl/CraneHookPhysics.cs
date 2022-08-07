using InputUI;
using Sounds;
using UIManagers;
using UnityEngine;
using UnityEngine.Events;

namespace CraneControl
{
    public class CraneHookPhysics : MonoBehaviour
    {
        public static readonly UnityEvent<LoseReason> OnJointBroken = new();
        [SerializeField] private Rigidbody rb;
        [SerializeField] private UIButton upButton, downButton;
        [SerializeField] private SpringJoint joint;
        
        [SerializeField] private float currentTolerance;
        [SerializeField] private float minTolerance, maxTolerance;
        [SerializeField] private float motorSpeed;

        [SerializeField] private SoundPlayController soundController;
        [SerializeField] private AudioClip liftUp, liftDown;

        private bool _jointIsBroken;

        private void Start()
        {
            joint.tolerance = currentTolerance;
        }

        private void Update()
        {
            if(_jointIsBroken == true) return;
            
            var value = upButton.GetButtonValue() + downButton.GetButtonValue();
            var keyboardValue = GetKeyboardInput();
            value += keyboardValue;
            value = Mathf.Clamp(value, -1f, 1f);
            SoundCheck(value);
            if (value == 0f) return;

            currentTolerance += value * motorSpeed * Time.deltaTime;
            currentTolerance = Mathf.Clamp(currentTolerance, minTolerance, maxTolerance);
            if (joint == null)
            {
                Debug.Log("Lose");
                _jointIsBroken = true;
                OnJointBroken.Invoke(LoseReason.RopeIsBroken);
                return;
            }
            joint.tolerance = currentTolerance;

            if(rb.IsSleeping() == true) rb.WakeUp();
        }

        private float GetKeyboardInput()
        {
            var value = 0f;
            if (Input.GetKey(KeyCode.UpArrow) == true) value--;
            if (Input.GetKey(KeyCode.DownArrow) == true) value++;
            return value;
        }

        private void SoundCheck(float value)
        {
            if ((value == 0f || currentTolerance >= maxTolerance || currentTolerance <= minTolerance))
            {
                soundController.Stop();
                return;
            }

            soundController.SetVolume(1f);
            
            switch (value)
            {
                case > 0f:
                    soundController.Play(liftDown);
                    break;
                case < 0f:
                    soundController.Play(liftUp);
                    break;
            }
        }
    }
}
