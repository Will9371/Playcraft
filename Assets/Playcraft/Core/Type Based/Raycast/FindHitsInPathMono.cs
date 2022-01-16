using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class FindHitsInPathMono : MonoBehaviour
    {
        [Serializable] public class PathWithHitsEvent : UnityEvent<Vector3[], List<IndexedRaycastHit>> { }
        [SerializeField] PathWithHitsEvent Output;
        
        FindHitsInPath hitsInPath;
        
        void Awake()
        {
            hitsInPath = new FindHitsInPath(transform);
        }

        public void Input(Vector3[] path)
        {
            Output.Invoke(path, hitsInPath.Input(path));
        }
    }
}