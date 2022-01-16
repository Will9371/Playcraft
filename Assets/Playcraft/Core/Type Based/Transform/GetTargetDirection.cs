using System;
using UnityEngine;

namespace Playcraft
{    
    [Serializable]
    public class GetTargetDirection
    {
        public Transform self;
        public Transform target;
        [SerializeField] DirectionalConstraints constraints;
            
        public Vector3 targetVector => target.position - self.position; 
        public Vector3 targetDirection => constraints.GetConstrainedDirection(targetVector);
        public float targetDistance => Vector3.Distance(target.position, self.position);
    }
}