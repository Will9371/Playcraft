using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class CheckCapsuleOverlap
    {
        public CapsuleCollider capsule;
        public List<Collider> ignored = new List<Collider>();
        
        public Vector3 capsuleCenter => capsule.transform.position + capsule.center;

        public bool StepBlocked(Vector3 step)
        {
            var hits = Physics.OverlapSphere(capsuleCenter + step, capsule.radius);
            
            foreach (var hit in hits)
                if (!ignored.Contains(hit) && !hit.isTrigger)
                    return true;
                    
            return false;
        }
    }
}
