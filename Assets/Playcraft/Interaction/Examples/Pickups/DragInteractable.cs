using UnityEngine;

namespace Playcraft
{
    public class DragInteractable : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] FollowInstant follow;
        [SerializeField] MessageLink messenger;
        #pragma warning restore 0649
        
        public bool active;
        
        public void SetSource(MessageLink value)
        {
            if (active) return;
            follow.SetTarget(value.transform);
        }
            
        public void Grab(bool value)
        {    
            active = value;
            follow.Follow(value);
            messenger.SetIgnoreLink(value); 
        }
        
        public void MessageSource(TagSO message)
        {
            if (active) return;
            messenger.Output(message);
        }
    }
}
