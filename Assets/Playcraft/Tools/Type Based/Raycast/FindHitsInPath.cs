using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindHitsInPath : MonoBehaviour
{
    [Serializable] public class PathWithHitsEvent : UnityEvent<Vector3[], List<IndexedRaycastHit>> { }
    [SerializeField] PathWithHitsEvent Output;

    public void Input(Vector3[] path)
    {
        var hits = new List<IndexedRaycastHit>();
        
        for (int i = 1; i < path.Length; i++)
        {
            var last = transform.TransformPoint(path[i - 1]);
            var current = transform.TransformPoint(path[i]);

            RaycastHit hit;
            var distance = Vector3.Distance(last, current);
            var direction = (current - last).normalized;

            if (Physics.Raycast(last, direction, out hit, distance))
                hits.Add(new IndexedRaycastHit(hit, i));
        }
        
        Output.Invoke(path, hits);
    }
}

public struct IndexedRaycastHit
{
    public IndexedRaycastHit(RaycastHit hit, int index)
    {
        this.hit = hit;
        this.index = index;
    }

    public RaycastHit hit;
    public int index;
}