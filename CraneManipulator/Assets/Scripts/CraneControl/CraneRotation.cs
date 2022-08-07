using Sounds;
using UnityEngine;

namespace CraneControl
{
    public class CraneRotation : MonoBehaviour
    {
        [SerializeField] private Joystick input;
        [SerializeField] private SoundPlayController soundController;
        [SerializeField] private AudioClip sound;
        [SerializeField] private float maxSpeed, acceleration, stoppingAcceleration;

        private float _currentSpeed;
        private void Update()
        {
            var value = input.Direction.x + GetKeyboardValue();
            value = Mathf.Clamp(value, -1f, 1f);
        
            _currentSpeed = value != 0f
                ? Mathf.Lerp(_currentSpeed, value * maxSpeed, Time.deltaTime * acceleration)
                : Mathf.Lerp(_currentSpeed, value * maxSpeed, Time.deltaTime * stoppingAcceleration);

            transform.Rotate(transform.up * (_currentSpeed * Time.deltaTime));
            
            SoundCall(value);
        }

        private float GetKeyboardValue()
        {
            var value = 0f;
            if (Input.GetKey(KeyCode.A) == true) value--;
            if (Input.GetKey(KeyCode.D) == true) value++;
            return value;
        }

        private void SoundCall(float inputValue)
        {
            if(inputValue != 0f)
                soundController.SetVolume(Mathf.Abs(inputValue));
            
            if (inputValue == 0f)
            {
                soundController.Stop();
            }
            else
            {
                soundController.Play(sound);
            }
        }
    }
}
