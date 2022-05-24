using UnityEngine;

namespace ZMD
{
    public class ToggleInteraction : MonoBehaviour
    {
        [SerializeField] SO activate;
        [SerializeField] SO deactivate;

        SO message => isActive ? deactivate : activate;
        GameObject source => isActive ? null : gameObject;

        ISetObject objectRelay;
        ISetObject cachedObjectRelay;
        ISetSO interactable;
        ISetSO cachedInteractable;
        bool isActive;

        public void SetInteractable(Collider value) 
        {
            var _interactable = value.GetComponent<ISetSO>();
            if (_interactable == null) return;
            interactable = _interactable;
            objectRelay = value.GetComponent<ISetObject>();

            cachedInteractable = interactable;
            cachedObjectRelay = objectRelay;
        }

        public void RemoveInteractable(Collider value) 
        { 
            var _interactable = value.GetComponent<ISetSO>();
            if (_interactable == null) return;
            interactable = null;
            objectRelay = null; 
        }

        // Toggle active/inactive with same event
        public void Interact()
        {
            if (objectRelay != null) 
                objectRelay.Message(source); 
            else if (isActive && cachedObjectRelay != null)
                cachedObjectRelay.Message(source);

            if (interactable != null)
                interactable.Message(message);
            else if (isActive && cachedInteractable != null)
                cachedInteractable.Message(message);
            else
                return;

            isActive = !isActive;
        }
        
        // Set active/inactive with different events
        public void Interact(bool value)
        {
            var _source = value ? gameObject : null;
            var _message = value ? activate : deactivate;
            objectRelay?.Message(_source);
            interactable?.Message(_message);
        }
    }
}
