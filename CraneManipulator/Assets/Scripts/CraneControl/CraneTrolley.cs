using Sounds;
using UnityEngine;

namespace CraneControl
{
    public class CraneTrolley : MonoBehaviour
    {
        [SerializeField] private Joystick input;

        [SerializeField] private Transform minLocalTransform, maxLocalTransform;
        [SerializeField] private Transform trolley;
        [SerializeField] private float speed, acceleration, stoppingAcceleration;
        [SerializeField] private SoundPlayController soundController;
        [SerializeField] private AudioClip sound;
        private float _currentSpeed;
        private float _lerp;

        private void Start()
        {
            trolley.position = Vector3.Lerp(minLocalTransform.position, maxLocalTransform.position, _lerp);
        }

        private void Update()
        {
            var value = input.Direction.y + GetKeyboardValue();
            value = Mathf.Clamp(value, -1f, 1f);

            _currentSpeed = Mathf.Lerp(_currentSpeed, speed * value,
                Time.time * (value != 0f ? acceleration : stoppingAcceleration));

            _lerp += _currentSpeed * Time.deltaTime;
            _lerp = Mathf.Clamp01(_lerp);

            trolley.position = Vector3.Lerp(minLocalTransform.position, maxLocalTransform.position, _lerp);

            SoundCall(value);
        }

        private float GetKeyboardValue()
        {
            var value = 0f;
            if (Input.GetKey(KeyCode.W) == true) value++;
            if (Input.GetKey(KeyCode.S) == true) value--;
            return value;
        }

        private void SoundCall(float inputValue)
        {
            if (_lerp is <= 0f or >= 1f)
                inputValue = 0f;
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
