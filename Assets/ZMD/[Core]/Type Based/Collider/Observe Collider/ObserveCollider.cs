using System;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class ObserveCollider
    {
        public ObservedCollider[] observedColliders;

        public void SetEnterTrigger(Action<Collider, Collider> enter)
        {
            foreach (var observed in observedColliders)
                observed.onTriggerEnter = enter;
        } 
        
        public void SetExitTrigger(Action<Collider, Collider> exit)
        {
            foreach (var observed in observedColliders)
                observed.onTriggerExit = exit;
        }
        
        public void SetStayTrigger(Action<Collider, Collider> stay)
        {
            foreach (var observed in observedColliders)
                observed.onTriggerStay = stay;
        }
        
        public void SetEnterCollision(Action<Collider, Collision> enter)
        {
            foreach (var observed in observedColliders)
                observed.onCollisionEnter = enter;
        } 
        
        public void SetExitCollision(Action<Collider, Collision> exit)
        {
            foreach (var observed in observedColliders)
                observed.onCollisionExit = exit;
        }
        
        public void SetStayCollision(Action<Collider, Collision> stay)
        {
            foreach (var observed in observedColliders)
                observed.onCollisionStay = stay;
        }
    }
}
