using UnityEngine;

// REFACTOR: unclear SRP, reliance on inheritance antipattern
// Consider extracting collision path into separate scripts
namespace Playcraft
{
    public class RespondToTouchBase : MonoBehaviour
    {
        #region Trigger
    
        void OnTriggerEnter(Collider other) { InputTrigger(other, TouchType.Begin, CollisionType.Trigger); }
        void OnTriggerExit(Collider other) { InputTrigger(other, TouchType.End, CollisionType.Trigger); }
        void OnTriggerStay(Collider other) { InputTrigger(other, TouchType.Continuous, CollisionType.Trigger); }
        
        protected virtual void InputTrigger(Collider other, TouchType touchType, CollisionType collisionType) { }
        
        #endregion
        
        #region Collision

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
        
        protected virtual void InputCollision(Collision other, TouchType touchType) { }
        
        #endregion
        
        #region Trigger2D
        
        void OnTriggerEnter2D(Collider2D other) { InputTrigger(other, TouchType.Begin, CollisionType.Trigger); }

        void OnTriggerExit2D(Collider2D other) { InputTrigger(other, TouchType.End, CollisionType.Trigger); }
        void OnTriggerStay2D(Collider2D other) { InputTrigger(other, TouchType.Continuous, CollisionType.Trigger); }
        
        protected virtual void InputTrigger(Collider2D other, TouchType touchType, CollisionType collisionType) { }
        
        #endregion
    }
}
