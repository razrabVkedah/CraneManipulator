using UnityEngine;
using UnityEngine.Events;

namespace InteractableObjects
{
    public class TaskPlace : MonoBehaviour
    {
        public static readonly UnityEvent OnTaskPlaceFilling = new();

        [SerializeField] private Material ghostMaterial;
        
        [SerializeField] private Color emptyColor = Color.red, fillColor = Color.green;
        [SerializeField]private Renderer placeRenderer;

        public void SetRenderer(Renderer newRenderer)
        {
            placeRenderer = newRenderer;
            placeRenderer.material = ghostMaterial;
        }

        [SerializeField] private int objectIndex;
        
        public void SetObjectIndex(int index) => objectIndex = index;

        public int GetPlaceIndex() => objectIndex;

        public bool IsEmpty { get; private set; } = true;

        public bool PlaceObject(int index, Transform taskObject, Rigidbody taskObjectRb)
        {
            if (IsEmpty == false || index != objectIndex) return false;

            taskObject.position = transform.position;
            taskObject.rotation = transform.rotation;
            taskObjectRb.velocity = Vector3.zero;
            taskObjectRb.angularVelocity = Vector3.zero;
            IsEmpty = false;
            placeRenderer.material.color = fillColor;
            OnTaskPlaceFilling.Invoke();
            return true;
        }

        public void RemoveObject(int index)
        {
            if (IsEmpty == true || index != objectIndex) return;

            IsEmpty = true;
            placeRenderer.material.color = emptyColor;
            OnTaskPlaceFilling.Invoke();
        }
    }
}
