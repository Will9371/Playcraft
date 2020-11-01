using System;
using UnityEngine;

namespace Playcraft
{
    public class RespondToTouch : MonoBehaviour
    { 
        #pragma warning disable 0649   
        [SerializeField] ContactTriggerIO[] triggerEvents;
        [SerializeField] ContactCollisionIO[] collisionEvents;
        #pragma warning restore 0649
        
        void OnTriggerEnter(Collider other)
        {
            InputTrigger(other, TouchType.Begin, CollisionType.Trigger);
        }
        
        void OnTriggerExit(Collider other)
        {
            InputTrigger(other, TouchType.End, CollisionType.Trigger);
        }
        
        void OnTriggerStay(Collider other)
        {
            InputTrigger(other, TouchType.Continuous, CollisionType.Trigger);
        }
        
        void InputTrigger(Collider other, TouchType touchType, CollisionType collisionType)
        {
            foreach (var item in triggerEvents)
                item.RequestActivate(other, touchType, collisionType);        
        }
        
        void OnCollisionEnter(Collision other) 
        {
            InputTrigger(other.collider, TouchType.Begin, CollisionType.Collision); 
            InputCollision(other, TouchType.Begin);
        }
        
        void OnCollisionExit(Collision other) 
        {
            InputTrigger(other.collider, TouchType.End, CollisionType.Collision); 
            InputCollision(other, TouchType.End); 
        }
        
        void OnCollisionStay(Collision other) 
        {
            InputTrigger(other.collider, TouchType.Continuous, CollisionType.Collision);
            InputCollision(other, TouchType.Continuous); 
        }
        
        void InputCollision(Collision other, TouchType touchType)
        {
            foreach (var item in collisionEvents)
                item.RequestActivate(other, touchType);
        }
        
        [Serializable] class ContactTriggerIO
        {
            #pragma warning disable 0649
            [SerializeField] TouchType touchType;
            [SerializeField] CollisionType collisionType;
            [SerializeField] CollisionType otherColliderType;
            [SerializeField] ColliderEvent Output;
            #pragma warning restore 0649

            public void RequestActivate(Collider other, TouchType touchType, CollisionType collisionType)
            {
                if (this.collisionType != CollisionType.Any && this.collisionType != collisionType ||
                    other.isTrigger && otherColliderType == CollisionType.Collision ||
                    !other.isTrigger && otherColliderType == CollisionType.Trigger)
                    return;
            
                if (this.touchType == touchType)
                    Output.Invoke(other);
            }
        }
        
        [Serializable] class ContactCollisionIO
        {
            #pragma warning disable 0649
            [SerializeField] TouchType touchType;
            [SerializeField] CollisionEvent Output;
            #pragma warning restore 0649

            public void RequestActivate(Collision other, TouchType touchType)
            {            
                if (this.touchType == touchType)
                    Output.Invoke(other);
            }
        }
        
        enum CollisionType { Any, Trigger, Collision }
        enum TouchType { Begin, End, Continuous }
    }
}
