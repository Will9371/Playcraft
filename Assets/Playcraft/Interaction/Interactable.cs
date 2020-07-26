using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class Interactable : MonoBehaviour
    {    
        #pragma warning disable 0649
        [SerializeField] GameObjectTagEvent EventsWithSource;
        [SerializeField] GameObjectEvent OnSetInteractor;
        [SerializeField] UnityEvent OnLoseInteractor;
        #pragma warning restore 0649
        
        GameObject interactor;
        
        [SerializeField] bool inputLocked = false;
        public void SetInputLock(bool value) { inputLocked = value; }
        
        public bool interactorLocked = false;
        public void SetInteractorLock(bool value) { interactorLocked = value; }
        
        public void Input(TagSO id)
        {
            if (inputLocked) return;
            if (interactor) EventsWithSource.Invoke(id, interactor);
        }
        
        public void SetInteractor(Collider other) { SetInteractor(other.gameObject); }
        public void SetInteractor(GameObject other) 
        {
            if (interactorLocked) return; 
            interactor = other;
            OnSetInteractor.Invoke(other); 
        }
        
        public void ClearInteractor() 
        {
            if (interactorLocked) return;  
            interactor = null; 
            OnLoseInteractor.Invoke();
        }
    }
}

//[SerializeField] GameObjectEvent Activate;
//[SerializeField] GameObjectEvent Deactivate;

//bool active;    

/*public void Input(bool active)
{
    this.active = active;
    if (!interactor) return;

    if (active && interactor != null)
        Activate.Invoke(interactor);
    else if (!active && interactor != null)
        Deactivate.Invoke(interactor);
}*/

//if (interactor != null && active)
//    Deactivate.Invoke(interactor);