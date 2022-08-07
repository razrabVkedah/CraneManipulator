using UnityEngine;

namespace CraneControl
{
    public class CraneVisualize : MonoBehaviour
    {
        [SerializeField] private Transform maxTrolleyTransform;
        [SerializeField] private Transform minTrolleyTransform;
        [SerializeField] private float circleOffset;
        
        [SerializeField] private float mTheta = 0.1f;

  
        void OnDrawGizmos()
        {
            if (maxTrolleyTransform != null)
            {
                var toMax = maxTrolleyTransform.position - transform.position;
                var maxRadius = new Vector2(toMax.x, toMax.z).magnitude;
                DrawCircle(maxRadius, Color.red);
            }

            if (minTrolleyTransform != null)
            {
                var toMin = minTrolleyTransform.position - transform.position;
                var minRadius = new Vector2(toMin.x, toMin.z).magnitude;
                DrawCircle(minRadius, Color.red);
            }
        }

        private void DrawCircle(float radius, Color color)
        {
            if (mTheta < 0.0001f) mTheta = 0.0001f;
 
            // set matrix
            var defaultMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
 
            // set the color
            var defaultColor = Gizmos.color;
            Gizmos.color = color;
 
            // Draw circle
            var beginPoint = Vector3.zero;
            var firstPoint = Vector3.zero;


            for (float theta = 0; theta < 2 * Mathf.PI; theta += mTheta)
            {
                var x = radius * Mathf.Cos(theta);
                var z = radius * Mathf.Sin(theta);
                var endPoint = new Vector3(x, circleOffset, z);
                if (theta == 0)
                {
                    firstPoint = endPoint;
                }
                else
                {
                    Gizmos.DrawLine(beginPoint, endPoint);
                }
                beginPoint = endPoint;
            }
 
            // Draw the last segment
            Gizmos.DrawLine(firstPoint, beginPoint);
 
            // restore default colors
            Gizmos.color = defaultColor; 
 
            // restore the default matrix
            Gizmos.matrix = defaultMatrix;
        }

    }
}
