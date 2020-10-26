/*
using System;
using UnityEngine;
using UnityEngine.Events;

// OBSOLETE
namespace Playcraft
{
    [Serializable] public class MessageLinkEvent : UnityEvent<MessageLink> { }

    public class MessageLink : MonoBehaviour
    {
        #pragma warning disable 0649
        [Tooltip("Relays linked object when link set, null when cleared")]
        [SerializeField] MessageLinkEvent RelaySource;
        [Tooltip("Relays incoming messages")]
        [SerializeField] TagEvent RelayMessage;
        [Tooltip("Defines responses to specific messages")]
        [SerializeField] EventResponder responses;
        #pragma warning restore 0649

        [Header("Visible for debug")]
        public MessageLink link;
        
        public bool ignoreInput;
        public void SetIgnoreInput(bool value) { ignoreInput = value; }
        public bool ignoreLink;
        public void SetIgnoreLink(bool value) { ignoreLink = value; }
        
        public void TrySetLink(Collider value) 
        { 
            if (value == null) ClearLink();
            else TrySetLink(value.gameObject); 
        }
        
        public void TrySetLink(RaycastHit value) { TrySetLink(value.transform.gameObject); }
        public void TrySetLink(GameObject value)
        {
            var _link = value.GetComponent<MessageLink>();
            if (_link != null) SetLink(_link);
        }
        
        public void SetLink(MessageLink value) 
        {
            if (ignoreLink) return;   
            link = value; 
            RelaySource.Invoke(value); 
        }
        
        public void ClearLink() 
        {
            if (ignoreLink) return;     
            link = null; 
            RelaySource.Invoke(null);
        }
        
        // verify needed
        public void TryOutput(SO message)
        {
            if (ignoreLink) return;
            Output(message);
        }

        public void Output(SO message)
        {
            if (link == null) return;
            link.Input(this, message);
        }
        
        public void Output(SO message, MessageLink recipient)
        {
            recipient.Input(message);
        }
        
        public void Input(MessageLink source, SO message)
        {
            if (ignoreInput) return;
            SetLink(source);
            React(message);
        }
        
        public void Input(SO message)
        {
            if (ignoreInput) return;
            React(message);
        }
        
        void React(SO message)
        {
            var response = responses.GetResponse(message);
            response?.Invoke(); 
            RelayMessage.Invoke(message);             
        }
    }
}
*/