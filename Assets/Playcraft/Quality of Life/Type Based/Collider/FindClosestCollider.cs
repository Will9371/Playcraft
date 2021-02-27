using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FindClosestCollider : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] ColliderEvent Output;
        
        Find_Closest_Collider process;
        
        void Awake() { process = new Find_Closest_Collider(self); }

        public void Input(List<Collider> others) { Output.Invoke(process.Input(others)); }
    }

    public class Find_Closest_Collider
    {
        Transform self;
            
        public Find_Closest_Collider(Transform self) { this.self = self; }

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
