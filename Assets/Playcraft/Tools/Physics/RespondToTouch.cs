using System;
using UnityEngine;

public class RespondToTouch : MonoBehaviour
{    
    [SerializeField] ContactTriggerIO[] triggerEvents;
    [SerializeField] ContactCollisionIO[] collisionEvents;
    
    private void OnTriggerEnter(Collider other)
    {
        InputTrigger(other, PressType.Begin, CollisionType.Trigger);
    }
    
    private void OnTriggerExit(Collider other)
    {
        InputTrigger(other, PressType.End, CollisionType.Trigger);
    }
    
    private void OnTriggerStay(Collider other)
    {
        InputTrigger(other, PressType.Continuous, CollisionType.Trigger);
    }
    
    private void InputTrigger(Collider other, PressType pressType, CollisionType collisionType)
    {
        foreach (var item in triggerEvents)
            item.RequestActivate(other, pressType, collisionType);        
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        InputTrigger(other.collider, PressType.Begin, CollisionType.Collision); 
        InputCollision(other, PressType.Begin);
    }
    
    private void OnCollisionExit(Collision other) 
    {
        InputTrigger(other.collider, PressType.End, CollisionType.Collision); 
        InputCollision(other, PressType.End); 
    }
    
    private void OnCollisionStay(Collision other) 
    {
        InputTrigger(other.collider, PressType.Continuous, CollisionType.Collision);
        InputCollision(other, PressType.Continuous); 
    }
    
    private void InputCollision(Collision other, PressType pressType)
    {
        foreach (var item in collisionEvents)
            item.RequestActivate(other, pressType);
    }
    
    [Serializable] class ContactTriggerIO
    {
        [SerializeField] PressType touchType;
        [SerializeField] CollisionType collisionType;
        [SerializeField] ColliderEvent Output;

        public void RequestActivate(Collider other, PressType touchType, CollisionType collisionType)
        {
            if (this.collisionType != CollisionType.Any && this.collisionType != collisionType)
                return;
        
            if (this.touchType == touchType)
                Output.Invoke(other);
        }
    }
    
    [Serializable] class ContactCollisionIO
    {
        [SerializeField] PressType touchType;
        [SerializeField] CollisionEvent Output;

        public void RequestActivate(Collision other, PressType touchType)
        {
            if (this.touchType == touchType)
                Output.Invoke(other);
        }
    }
    
    enum CollisionType { Any, Trigger, Collision }
}
