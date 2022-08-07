using UnityEngine;

namespace CraneControl
{
    public class CraneRope : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;
        [SerializeField] private Transform topPart, bottomPart;
        private void Start()
        {
            line.gameObject.transform.position = Vector3.zero;
            line.positionCount = 2;
        }

        private void Update()
        {
            var positions = new[] {topPart.position, bottomPart.position};
            line.SetPositions(positions);
        }
    }
}
