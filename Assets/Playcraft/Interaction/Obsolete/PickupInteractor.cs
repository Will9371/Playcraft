/*
using UnityEngine;
using UnityEngine.Events;

// OBSOLETE
namespace Playcraft
{
    public class PickupInteractor : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] MessageLink messenger;
        [SerializeField] SO handFreeMessage;
        [SerializeField] SO handFullMessage;
        [SerializeField] UnityEvent OnDrop;
        #pragma warning restore 0649
        
        bool isHolding;
        
        public void SetHoldingAndInteract(bool value)
        {
            SetHolding(value);
            Interact();
        }

        public void SetHolding(bool value) 
        { 
            isHolding = value; 
            messenger.SetIgnoreLink(value);
            
            if (!value) 
            {
                messenger.ClearLink();
                OnDrop.Invoke();
            }
        }
        
        public void Interact()
        {
            var message = isHolding ? handFullMessage : handFreeMessage;
            messenger.Output(message);
        }
    }
}
*/