using UnityEngine;
using UnityEngine.Events;

namespace InteractableObjects
{
    public class DefaultObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform connectTransform;

        [Header("Break settings")]
        [SerializeField] private float speedForBreak = 7.5f;

        private FixedJoint _fixedJoint;
        private bool _destroyed;
        private bool _onHook;

        #region Events
        private readonly UnityEvent _onObjectDestroy = new();
        private readonly UnityEvent _onObjectDrop = new();
        private readonly UnityEvent _onObjectTake = new();

        public void AddActionOnDestroy(UnityAction action)
        {
            _onObjectDestroy.AddListener(action);
        }

        public void RemoveActionOnDestroy(UnityAction action)
        {
            _onObjectDestroy.RemoveListener(action);
        }

        public void AddActionOnTake(UnityAction action)
        {
            _onObjectTake.AddListener(action);
        }

        public void RemoveActionOnTake(UnityAction action)
        {
            _onObjectTake.RemoveListener(action);
        }

        public void AddActionOnDrop(UnityAction action)
        {
            _onObjectDrop.AddListener(action);
        }

        public void RemoveActionOnDrop(UnityAction action)
        {
            _onObjectDrop.RemoveListener(action);
        }

        #endregion
        
        public bool Destroyed() => _destroyed;
        public bool IsDangerousVelocity() => rb.velocity.magnitude >= speedForBreak;

        #region Take_Drop
        public void Take(Vector3 hookPosition, Rigidbody hookRb)
        {
            if(_destroyed == true) return;
            
            //var toHook = hookPosition - connectTransform.position;
            //transform.position += toHook;
            
            _onObjectTake.Invoke();

            if (_fixedJoint != null) Destroy(_fixedJoint);
            
            _fixedJoint = gameObject.AddComponent<FixedJoint>();
            _fixedJoint.connectedBody = hookRb;
        }

        public void Drop()
        {
            if(_fixedJoint == null) return;
            
            _onObjectDrop.Invoke();
            Destroy(_fixedJoint);
            _fixedJoint = null;
        }
        #endregion

        #region Break_Collision
        public void Break()
        {
            if(_destroyed == true) return;

            _onObjectDestroy.Invoke();
            _destroyed = true;
            Destroy(gameObject, 0.5f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.relativeVelocity.magnitude < speedForBreak) return;
            
            Break();
        }
        #endregion

        #region EditorFunctions

        private void Reset()
        {
            gameObject.layer = 8;
            if (GetComponent<Collider>() == null)
            {
                var col = gameObject.AddComponent<MeshCollider>();
                col.convex = true;
            }

            rb = GetComponent<Rigidbody>();
            if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
            rb.angularDrag = 10f;
            rb.drag = 1f;
            if (GetComponent<TaskObject>() == null) gameObject.AddComponent<TaskObject>();
            var hasConnectTransform = false;
            for (var i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.name != "ConnectTransform") continue;
                connectTransform = transform.GetChild(i);
                hasConnectTransform = true;
                break;
            }
            if (hasConnectTransform == true) return;
            
            var newGo = new GameObject
            {
                transform =
                {
                    position = transform.position,
                    parent = transform
                },
                name = "ConnectTransform"
            };

            connectTransform = newGo.transform;
        }
        #endregion
    }
}
