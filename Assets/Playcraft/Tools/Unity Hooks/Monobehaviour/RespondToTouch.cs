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
        
        private void OnTriggerEnter(Collider other)
        {
            InputTrigger(other, TouchType.Begin, CollisionType.Trigger);
        }
        
        private void OnTriggerExit(Collider other)
        {
            InputTrigger(other, TouchType.End, CollisionType.Trigger);
        }
        
        private void OnTriggerStay(Collider other)
        {
            InputTrigger(other, TouchType.Continuous, CollisionType.Trigger);
        }
        
        private void InputTrigger(Collider other, TouchType touchType, CollisionType collisionType)
        {
            foreach (var item in triggerEvents)
                item.RequestActivate(other, touchType, collisionType);        
        }
        
        private void OnCollisionEnter(Collision other) 
        {
            InputTrigger(other.collider, TouchType.Begin, CollisionType.Collision); 
            InputCollision(other, TouchType.Begin);
        }
        
        private void OnCollisionExit(Collision other) 
        {
            InputTrigger(other.collider, TouchType.End, CollisionType.Collision); 
            InputCollision(other, TouchType.End); 
        }
        
        private void OnCollisionStay(Collision other) 
        {
            InputTrigger(other.collider, TouchType.Continuous, CollisionType.Collision);
            InputCollision(other, TouchType.Continuous); 
        }
        
        private void InputCollision(Collision other, TouchType touchType)
        {
            foreach (var item in collisionEvents)
                item.RequestActivate(other, touchType);
        }
        
        [Serializable] class ContactTriggerIO
        {
            #pragma warning disable 0649
            [SerializeField] TouchType touchType;
            [SerializeField] CollisionType collisionType;
            [SerializeField] ColliderEvent Output;
            #pragma warning restore 0649

            public void RequestActivate(Collider other, TouchType touchType, CollisionType collisionType)
            {
                if (this.collisionType != CollisionType.Any && this.collisionType != collisionType)
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
