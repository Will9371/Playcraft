/*
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class FindClosestCollider
    {
        [SerializeField] Transform self;
        
        Collider closest;
        float shortestDistance;
        float distance;

        public Collider Input(List<Collider> others)
        {
            closest = null;
            shortestDistance = Mathf.Infinity;
                
            foreach (var other in others)
            {
                distance = Vector3.Distance(self.position, other.transform.position);
                if (distance >= shortestDistance) continue;
                    
                closest = other;
                shortestDistance = distance;
            }
                
            return closest;
        }        
    }
}
*/
