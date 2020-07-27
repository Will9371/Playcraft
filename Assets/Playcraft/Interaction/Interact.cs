using UnityEngine;

namespace Playcraft
{
    public class Interact : MonoBehaviour
    {        
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
            //Debug.Log(other);
            if (interactionLocked) return;
        
            var _interactable = other.GetComponent<Interactable>();
            if (!_interactable || _interactable.interactorLocked) return;
            interactable = _interactable;
            
            if (activate) interactable.SetInteractor(gameObject);
            else interactable.ClearInteractor();
        }
        
        public void Activate(TagSO value)
        {
            if (!interactable) return;
            interactable.Input(value);
        }
    }
}