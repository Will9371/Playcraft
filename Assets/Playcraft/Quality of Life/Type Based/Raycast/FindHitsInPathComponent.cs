using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class FindHitsInPathComponent : MonoBehaviour
    {
        [Serializable] public class PathWithHitsEvent : UnityEvent<Vector3[], List<IndexedRaycastHit>> { }
        [SerializeField] PathWithHitsEvent Output = default;
        
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