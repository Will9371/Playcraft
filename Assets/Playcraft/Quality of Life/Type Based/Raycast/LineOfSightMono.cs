using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class LineOfSightMono : MonoBehaviour
    {
        [SerializeField] Transform source;
        [SerializeField] ColliderListEvent Output;
        [Tooltip("True: only detect colliders with TransformChildContainer component attached.  " +
                 "False: raycast to center of colliders without TransformChildContainer.")]
        [SerializeField] bool requireMultiplePoints;
        [Tooltip("True: draw debug lines for all raycasts.")]
        [SerializeField] bool debug;
        
        LineOfSight process;
                
        void Awake() { process = new LineOfSight(source, requireMultiplePoints, debug); }
        
        public void Input(List<Collider> values) { Output.Invoke(process.Input(values)); }
    }
}
