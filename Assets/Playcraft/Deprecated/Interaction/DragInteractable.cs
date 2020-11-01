/*
using UnityEngine;

namespace Playcraft
{
    // DEPRECATE
    public class DragInteractable : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] FollowInstant follow;
        [SerializeField] MessageLink messenger;
        #pragma warning restore 0649
        
        public bool active;
        
        public void SetSource(GameObject value)
        {
            if (active) return;
            follow.SetTarget(value.transform);
        }
        
        // REMOVE
        public void SetSource(MessageLink value)
        {
            if (active) return;
            follow.SetTarget(value.transform);
        }
            
        public void Grab(bool value)
        {    
            active = value;
            follow.Follow(value);
            //messenger.SetIgnoreLink(value); 
        }
        
        // REMOVE
        public void MessageSource(SO message)
        {
            if (active) return;
            messenger.Output(message);
        }
    }
}
*/