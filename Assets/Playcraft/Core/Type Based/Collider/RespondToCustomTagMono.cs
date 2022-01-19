using System;
using UnityEngine;

// REFACTOR: unclear SRP, reliance on inheritance antipattern
// Consider extracting collision path into separate scripts
namespace Playcraft
{
    public class RespondToCustomTagMono : RespondToTouchBase
    {
        [SerializeField] TriggerBinding[] triggerBindings;
        [SerializeField] CollisionBinding[] collisionBindings;
        [SerializeField] Trigger2DBinding[] trigger2DBindings;
    
        protected override void InputTrigger(Collider other, TouchType touchType, CollisionType collisionType)
        {
            foreach (var item in triggerBindings)
                item.Input(other, touchType, collisionType);        
        }
        
        protected override void InputTrigger(Collider2D other, TouchType touchType, CollisionType collisionType)
        {
            foreach (var item in trigger2DBindings)
                item.Input(other, touchType, collisionType);        
        }
        
        protected override void InputCollision(Collision other, TouchType touchType)
        {
            foreach (var item in collisionBindings)
                item.Input(other, touchType);
        }
        
        public void SetTriggerTags(SO[] tags, int index = 0) { triggerBindings[index].SetTags(tags); }
        
        [Serializable] class TriggerBinding
        {
            [SerializeField] FilterTrigger touchFilter;
            [SerializeField] RespondToCustomTag tagFilter;
            [SerializeField] ColliderEvent response;
            
            public void Input(Collider other, TouchType touchType, CollisionType collisionType)
            {
                if (touchFilter.RequestActivate(other, touchType, collisionType) && tagFilter.Input(other))
                    response.Invoke(other);
            }
            
            public void SetTags(SO[] values) { tagFilter.validTags = values; }
        }
        
        [Serializable] class Trigger2DBinding
        {
            [SerializeField] FilterTrigger touchFilter;
            [SerializeField] RespondToCustomTag tagFilter;
            [SerializeField] Collider2DEvent response;
            
            public void Input(Collider2D other, TouchType touchType, CollisionType collisionType)
            {
                if (touchFilter.RequestActivate(other, touchType, collisionType) && tagFilter.Input(other))
                    response.Invoke(other);
            }
            
            public void SetTags(SO[] values) { tagFilter.validTags = values; }
        }
        
        [Serializable] class CollisionBinding
        {
            [SerializeField] FilterCollision touchFilter;
            [SerializeField] RespondToCustomTag tagFilter;
            [SerializeField] CollisionEvent response;
            
            public void Input(Collision other, TouchType touchType)
            {
                if (touchFilter.RequestActivate(other, touchType) && tagFilter.Input(other.collider))
                    response.Invoke(other);
            }
        }
    }
}