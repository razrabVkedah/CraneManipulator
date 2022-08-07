using UIManagers;
using UnityEngine;

namespace CraneControl
{
    public class ForceController : MonoBehaviour
    {
        [SerializeField] private SpringJoint hookJoint;
        [SerializeField] private ForceDisplay display;

        private void Update()
        {
            if(hookJoint == null) return;

            display.VisualizeSettings(hookJoint.currentForce.magnitude / hookJoint.breakForce);
        }
    }
}
