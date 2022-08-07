using System.Collections;
using UIManagers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace InteractableObjects
{
    public class TaskObject : MonoBehaviour
    {
        public static readonly UnityEvent<LoseReason> OnObjectDestroyEvent = new();
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float afterTakeTriggerIgnoreDelay = 0.5f;
        private IInteractable _interactable;
        private bool _ignoreTrigger;
        [SerializeField] private int objectIndex;

        private TaskPlace _touchedPlace;

        #region EditorFunctions
        [ContextMenu("Spawn task place")]
        private void SpawnTaskPlace()
        {
            var place = new GameObject
            {
                transform =
                {
                    position = transform.position + Vector3.up * 3f,
                    rotation = transform.rotation
                },
                name = gameObject.name + "_TaskPlace",
                layer = 9
            };

            var placeRenderer = place.AddComponent<MeshRenderer>();
            var myRenderer = GetComponent<MeshRenderer>();
            placeRenderer.sharedMaterial = myRenderer.sharedMaterial;
            placeRenderer.shadowCastingMode = ShadowCastingMode.Off;
            placeRenderer.receiveShadows = false;

            var placeMesh = place.AddComponent<MeshFilter>();
            placeMesh.sharedMesh = GetComponent<MeshFilter>().sharedMesh;

            var col = place.AddComponent<BoxCollider>();
            col.isTrigger = true;
  
            var taskPlace = place.AddComponent<TaskPlace>();
            taskPlace.SetObjectIndex(objectIndex);
            taskPlace.SetRenderer(placeRenderer);
        }
        private void Reset()
        {
            rb = GetComponent<Rigidbody>();
            
            if(rb == null) Debug.LogError(gameObject.name + " does NOT contains RigidbodyComponent");
        }
        #endregion
        
        private void Start()
        {
            _interactable = GetComponent<IInteractable>();
            _interactable.AddActionOnDestroy(OnObjectDestroy);
            _interactable.AddActionOnTake(OnTakeObject);
        }

        private void OnDestroy()
        {
            _interactable?.RemoveActionOnDestroy(OnObjectDestroy);
            _interactable?.RemoveActionOnTake(OnTakeObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_ignoreTrigger == true) return;
            
            if(_interactable.Destroyed() == true) return;
            
            if (_interactable.IsDangerousVelocity() == true)
            {
                _interactable.Break();
                return;
            }
            
            var place = other.gameObject.GetComponent<TaskPlace>();
            if(place == null) return;
            
            if(place.GetPlaceIndex() != objectIndex || place.IsEmpty == false) return;
            
            if (_touchedPlace != null)
            {
                _touchedPlace.RemoveObject(objectIndex);
                _touchedPlace = null;
            }

            if (place.PlaceObject(objectIndex, transform, rb) == false) return;

            _interactable.Drop();
            _touchedPlace = place;
        }
        private void OnObjectDestroy()
        {
            OnObjectDestroyEvent.Invoke(LoseReason.ObjectIsBroken);
            if(_touchedPlace == null) return;

            _touchedPlace.RemoveObject(objectIndex);

            _touchedPlace = null;
        }

        private void OnTakeObject()
        {
            StopAllCoroutines();
            Debug.Log("Take");
            StartCoroutine(Delay());
            if (_touchedPlace == null) return;
            _touchedPlace.RemoveObject(objectIndex);
            _touchedPlace = null;
        }

        private IEnumerator Delay()
        {
            _ignoreTrigger = true;
            yield return new WaitForSeconds(afterTakeTriggerIgnoreDelay);
            _ignoreTrigger = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if(_touchedPlace == null) return;
            
            var place = other.gameObject.GetComponent<TaskPlace>();
            if(place == null) return;

            if (_touchedPlace != place) return;
            
            _touchedPlace.RemoveObject(objectIndex);
            _touchedPlace = null;
        }
    }
}
