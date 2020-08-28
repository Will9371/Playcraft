using System;
using UnityEngine;
using UnityEngine.Events;

// DEPRECATE: use MessageLink
namespace Playcraft
{
    [Serializable] public class InteractorEvent : UnityEvent<Interactor> { }

    public class Interactable : MonoBehaviour
    {    
        #pragma warning disable 0649
        [SerializeField] GameObjectTagEvent EventsWithSource;    // DEPRECATE
        [SerializeField] TagEvent Relay;
        [SerializeField] InteractorEvent OnSetInteractor;
        [SerializeField] UnityEvent OnLoseInteractor;
        #pragma warning restore 0649
        
        Interactor interactor;
        
        [SerializeField] bool inputLocked = false;
        public void SetInputLock(bool value) { inputLocked = value; }
        
        public bool interactorLocked = false;
        public void SetInteractorLock(bool value) { interactorLocked = value; }
        
        public bool Input(TagSO id)
        {
            if (inputLocked || !interactor) return false;

            EventsWithSource.Invoke(id, interactor.gameObject);    // REMOVE
            Relay.Invoke(id);
            
            return true;
            //interactor.Input(id);
        }
        
        public void SetInteractor(Collider other) { SetInteractor(other.GetComponent<Interactor>()); }
        public void SetInteractor(GameObject other) { SetInteractor(other.GetComponent<Interactor>()); }
        public void SetInteractor(Interactor other) 
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