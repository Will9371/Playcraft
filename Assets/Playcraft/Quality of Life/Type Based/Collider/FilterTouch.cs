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
        {
            if (this.collisionType != CollisionType.Any && this.collisionType != collisionType ||
                other.isTrigger && otherColliderType == CollisionType.Collision ||
                !other.isTrigger && otherColliderType == CollisionType.Trigger)
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
