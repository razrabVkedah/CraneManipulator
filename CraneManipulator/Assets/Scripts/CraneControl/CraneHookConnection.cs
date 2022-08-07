using System.Collections.Generic;
using System.Linq;
using InteractableObjects;
using Sounds;
using UnityEngine;

namespace CraneControl
{
    public class CraneHookConnection : MonoBehaviour
    {
        [SerializeField] private Transform hookHolderTransform;
        [SerializeField] private Rigidbody hookRb;
        [SerializeField] private HookSoundController soundController;
        private IInteractable _connectedInteractable;
        private readonly List<GameObject> _touchedObjects = new();

        private float _lastVelocity;


        public void OnClickInteractButton()
        {
            if (_connectedInteractable != null)
            {
                Drop();
            }
            else
            {
                Take();
            }
        }

        private void Take()
        {
            foreach (var interactable in _touchedObjects
                         .Select(touchedObject => touchedObject.GetComponent<IInteractable>())
                         .Where(interactable => interactable != null))
            {
                _connectedInteractable = interactable;
                _connectedInteractable.Take(hookHolderTransform.position, hookRb);
                _connectedInteractable.AddActionOnDestroy(OnInteractableObjectDestroyAction);
                _connectedInteractable.AddActionOnDrop(Drop);
                soundController.TakeSound();
                break;
            }
        }

        private void Drop()
        {
            _connectedInteractable?.RemoveActionOnDestroy(OnInteractableObjectDestroyAction);
            _connectedInteractable?.RemoveActionOnDrop(Drop);
            _connectedInteractable?.Drop();
            _connectedInteractable = null;
            soundController.DropSound();
        }

        private void OnInteractableObjectDestroyAction()
        {
            _connectedInteractable = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            _touchedObjects.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            _touchedObjects.Remove(other.gameObject);
        }
    }
}
