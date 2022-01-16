using System;
using UnityEngine;

namespace Playcraft
{
    public class RespondToTouchSimple : RespondToTouchBase
    {
        [Header("Experimental")]
        [SerializeField] TriggerBinding[] triggerBindings;
        [SerializeField] CollisionBinding[] collisionBindings;
    
        protected override void InputTrigger(Collider other, TouchType touchType, CollisionType collisionType)
        {
            foreach (var item in triggerBindings)
                item.Input(other, touchType, collisionType);        
        }
        
        protected override void InputCollision(Collision other, TouchType touchType)
        {
            foreach (var item in collisionBindings)
                item.Input(other, touchType);
        }
        
        [Serializable] class TriggerBinding
        {
            [SerializeField] FilterTrigger filter;
            [SerializeField] ColliderEvent response;
            
            public void Input(Collider other, TouchType touchType, CollisionType collisionType)
            {
                if (filter.RequestActivate(other, touchType, collisionType))
                    response.Invoke(other);
            }
        }
        
        [Serializable] class CollisionBinding
        {
            [SerializeField] FilterCollision filter;
            [SerializeField] CollisionEvent response;
            
            public void Input(Collision other, TouchType touchType)
            {
                if (filter.RequestActivate(other, touchType))
                    response.Invoke(other);
            }
        }
    }
}
