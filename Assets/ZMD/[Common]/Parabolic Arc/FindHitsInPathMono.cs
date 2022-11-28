using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class FindHitsInPathMono : MonoBehaviour
    {
        [Serializable] public class PathWithHitsEvent : UnityEvent<Vector3[], List<IndexedRaycastHit>> { }
        [SerializeField] PathWithHitsEvent Output;
        
        FindHitsInPath process;
        void Awake() { process = new FindHitsInPath(transform); }
        public void Input(Vector3[] path) { Output.Invoke(path, process.Input(path)); }
    }
    
    public class FindHitsInPath
    {    
        Transform source;
        
        public FindHitsInPath(Transform source) { this.source = source; }

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