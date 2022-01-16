using UnityEngine;

namespace Playcraft
{
    public class GetTargetDirectionMono : MonoBehaviour
    {
        [SerializeField] GetTargetDirection process;
        [SerializeField] bool outputOnUpdate = true;
        [SerializeField] Vector3Event Output = default;
        
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
}