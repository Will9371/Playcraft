using System;
using UnityEngine;

namespace ZMD
{
    public class GetTargetDirectionMono : MonoBehaviour
    {
        [SerializeField] GetTargetDirection process;
        [SerializeField] bool outputOnUpdate = true;
        [SerializeField] Vector3Event Output;
        
        Transform target => process.target;
        Vector3 targetDirection => process.targetDirection;
        
        void Start() { process.self = transform; }

        void Update()
        {
            if (!target || !outputOnUpdate) return;
            Output.Invoke(targetDirection);
        }
        
        public void SetTarget(Transform target) { process.target = target; }
        public void GetDirection() { Output.Invoke(targetDirection); }
    }
    
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