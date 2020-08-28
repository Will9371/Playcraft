using UnityEngine;
using UnityEngine.Events;

// DEPRECATE: use MessageLink
namespace Playcraft
{
    public class Interactor : MonoBehaviour
    {
        //[SerializeField] TagEvent Relay = default;
        //public void Input(TagSO value) { Relay.Invoke(value); }
        [SerializeField] UnityEvent Success;
            
        Interactable interactable;
        
        bool interactionLocked;
        public void SetLock(bool value) { interactionLocked = value; }
    
        public void SetInteractable(RaycastHit hit) { SetInteractable(hit.collider.gameObject); }
        public void SetInteractable(Collider other) { SetInteractable(other.gameObject); }
        public void SetInteractable(GameObject other) { SetInteractable(other, true); }
        
        public void ClearInteractable(RaycastHit hit) { ClearInteractable(hit.collider.gameObject); }
        public void ClearInteractable(Collider other) { ClearInteractable(other.gameObject); }
        public void ClearInteractable(GameObject other) { SetInteractable(other, false); }
        
        public void SetInteractable(GameObject other, bool activate)
        {
            if (interactionLocked) return;
        
            var _interactable = other.GetComponent<Interactable>();
            if (!_interactable || _interactable.interactorLocked) return;
            interactable = _interactable;
            
            if (activate) interactable.SetInteractor(this);
            else interactable.ClearInteractor();
        }
        
        // Sends message to interactable
        public void Activate(TagSO value)
        {
            if (!interactable) return;
            var result = interactable.Input(value);
            if (result) Success.Invoke();
            //Relay.Invoke(value);
        }        
    }
}