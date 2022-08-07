using UnityEngine;
using UnityEngine.Events;

namespace InteractableObjects
{
    public interface IInteractable
    {
        public void AddActionOnDestroy(UnityAction action);
        public void RemoveActionOnDestroy(UnityAction action);
        
        
        public void AddActionOnTake(UnityAction action);
        public void RemoveActionOnTake(UnityAction action);
        

        public void AddActionOnDrop(UnityAction action);
        public void RemoveActionOnDrop(UnityAction action);
        
        public bool Destroyed();
        public bool IsDangerousVelocity();
        
        public void Take(Vector3 hookPosition, Rigidbody hookRb);

        public void Break();
        
        public void Drop();
    }
}
