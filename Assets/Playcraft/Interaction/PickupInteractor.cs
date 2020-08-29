﻿using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class PickupInteractor : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] MessageLink messenger;
        [SerializeField] TagSO handFreeMessage;
        [SerializeField] TagSO handFullMessage;
        [SerializeField] UnityEvent OnDrop;
        #pragma warning restore 0649
        
        public void SetHoldingAndInteract(bool value)
        {
            SetHolding(value);
            Interact();
        }

        bool isHolding;
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
