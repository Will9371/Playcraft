using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FindHitsInPath
    {    
        Transform source;
        
        public FindHitsInPath(Transform source)
        {
            this.source = source;
        }

        public List<IndexedRaycastHit> Input(Vector3[] path)
        {
            var hits = new List<IndexedRaycastHit>();
                
            for (int i = 1; i < path.Length; i++)
            {
                var last = source.TransformPoint(path[i - 1]);
                var current = source.TransformPoint(path[i]);

                RaycastHit hit;
                var distance = Vector3.Distance(last, current);
                var direction = (current - last).normalized;

                if (Physics.Raycast(last, direction, out hit, distance))
                    hits.Add(new IndexedRaycastHit(hit, i));
            }
                
            return hits;
        }
    }
}
