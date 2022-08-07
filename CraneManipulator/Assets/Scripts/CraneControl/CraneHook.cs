using InputUI;
using UnityEngine;

namespace CraneControl
{
    public class CraneHook : MonoBehaviour
    {
        [SerializeField] private UIButton upButton, downButton;
        [SerializeField] private float ropeWidth;
        [SerializeField] private float minRopeWidth, maxRopeWidth;
        [SerializeField] private float motorSpeed;
        [SerializeField] private float damper;

        [SerializeField] private Transform trolley;

        private void Start()
        {
            transform.position = GetMoveToPosition();
        }

        private void Update()
        {
            GetInput();
            
            var moveToBest = GetMoveToPosition();
            
            var moveToReal = GetCurrentPositionWithRopeWidth(moveToBest.x, moveToBest.z);

            transform.position = Vector3.Lerp(transform.position, moveToReal, Time.deltaTime * damper);
        }

        private void GetInput()
        {
            var value = upButton.GetButtonValue() + downButton.GetButtonValue();
            ropeWidth += value * motorSpeed * Time.deltaTime;
            ropeWidth = Mathf.Clamp(ropeWidth, minRopeWidth, maxRopeWidth);
        }

        private Vector3 GetCurrentPositionWithRopeWidth(float moveToX, float moveToZ)
        {
            var x = transform.position.x;
            var z = transform.position.z;
            var horLeg = (new Vector2(x, z) - new Vector2(moveToX, moveToZ)).magnitude;
            var vertLeg = Mathf.Sqrt(ropeWidth * ropeWidth - horLeg * horLeg);
            var y = trolley.position.y - vertLeg;
            return new Vector3(moveToX, y, moveToZ);
        }

        private Vector3 GetMoveToPosition()
        {
            var trolleyPosition = trolley.position;
            return new Vector3(trolleyPosition.x, trolleyPosition.y - ropeWidth, trolleyPosition.z);
        }
    }
}
