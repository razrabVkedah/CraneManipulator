using UnityEngine;

namespace UIManagers
{
    public class ForceDisplay : MonoBehaviour
    {
        [SerializeField] private Transform pivot;
        [SerializeField] private float minRotationZ, maxRotationZ;
        [SerializeField] private float arrowSpeed;
        private float _currentLerp;
        
        public void VisualizeSettings(float lerp)
        {
            _currentLerp = Mathf.Lerp(_currentLerp, lerp, Time.deltaTime * arrowSpeed);
            
            pivot.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(minRotationZ, maxRotationZ, _currentLerp));
        }
    }
}
