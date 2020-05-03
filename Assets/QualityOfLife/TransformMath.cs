using System.Collections.Generic;
using UnityEngine;

public static class TransformMath
{
    public static Transform GetClosest(List<Transform> list, Vector3 position)
    {
        var shortestDistance = Mathf.Infinity;
        var closest = list[0];
        
        for (int i = 0; i < list.Count; i++)
        {
            var distance = Vector3.Distance(list[i].position, position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = list[i];
            }
        }
        
        return closest;
    }
}
