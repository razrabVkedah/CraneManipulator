using UnityEngine;

namespace CameraControl
{
    public class CameraChanger : MonoBehaviour
    {
        [SerializeField] private GameObject[] cameras;
        private int _currentIndex;
        
        public void OnClickChangeCamera()
        {
            _currentIndex++;
            if (_currentIndex >= cameras.Length) _currentIndex = 0;
            for (var i = 0; i < cameras.Length; i++)
            {
                cameras[i].SetActive(i == _currentIndex);
            }
        }
    }
}
