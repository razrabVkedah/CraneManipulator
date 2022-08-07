using UnityEngine;

namespace CameraControl
{
    public class CameraFollowObject : MonoBehaviour
    {
        [SerializeField] private Transform follow;
        [SerializeField] private bool useGlobal;

        private void Update()
        {
            if (useGlobal == true)
            {
                transform.position = follow.transform.position;
            }
            else
            {
                transform.localPosition = follow.localPosition;
            }
        }
    }
}
