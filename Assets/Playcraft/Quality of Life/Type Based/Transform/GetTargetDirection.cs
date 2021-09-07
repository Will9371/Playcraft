using System;
using UnityEngine;

namespace Playcraft
{
    public class GetTargetDirection : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] bool outputOnUpdate = true;
        [SerializeField] DirectionalConstraints constraints;
        
        public void SetTarget(Transform target) { this.target = target; }
        
        [SerializeField] Vector3Event Output = default;
        
        Vector3 targetVector => target.position - transform.position; 
        Vector3 targetDirection => constraints.GetConstrainedDirection(targetVector);

        void Update()
        {
            if (!target || !outputOnUpdate) return;
            Output.Invoke(targetDirection);
        }
        
        public void GetDirection() { Output.Invoke(targetDirection); }
    }
}