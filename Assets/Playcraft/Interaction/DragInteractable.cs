using Playcraft;
using UnityEngine;

// FAILED: consider remove
public class DragInteractable : MonoBehaviour
{
    [SerializeField] FollowInstant follow;
    [SerializeField] MessageLink messenger;
    
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
