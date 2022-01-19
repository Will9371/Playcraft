using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class FilterTrigger
    {
        [SerializeField] TouchType touchType;
        [SerializeField] CollisionType collisionType;
        [SerializeField] CollisionType otherColliderType;

        public bool RequestActivate(Collider other, TouchType touchType, CollisionType collisionType) 
        { return RequestActivate(other.isTrigger, touchType, collisionType); }
        
        public bool RequestActivate(Collider2D other, TouchType touchType, CollisionType collisionType)
        { return RequestActivate(other.isTrigger, touchType, collisionType); }
        
        public bool RequestActivate(bool isTrigger, TouchType touchType, CollisionType collisionType)
        {
            if (this.collisionType != CollisionType.Any && this.collisionType != collisionType ||
                isTrigger && otherColliderType == CollisionType.Collision ||
                !isTrigger && otherColliderType == CollisionType.Trigger)
                return false;
            
            return this.touchType == touchType;
        }        
    }
        
    // Handles collision events only, outputs full collision data
    [Serializable] public class FilterCollision
    {
        [SerializeField] TouchType touchType;

        public bool RequestActivate(Collision other, TouchType touchType)
        {            
            return this.touchType == touchType;
        }
    }
        
    public enum CollisionType { Any, Trigger, Collision }
    public enum TouchType { Begin, End, Continuous }
}
