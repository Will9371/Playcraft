using System.Collections.Generic;
using UnityEngine;

public class FindClosestCollider : MonoBehaviour
{
    [SerializeField] Transform self;
    [SerializeField] ColliderEvent Output;

    Collider closest;
    float shortestDistance;
    float distance;

    public void Input(List<Collider> others)
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
        
        Output.Invoke(closest);
    }
}
