using UnityEngine;

namespace Playcraft
{
    public class GetTargetDirection : MonoBehaviour
    {
        [SerializeField] Transform target;
        public void SetTarget(Transform target) { this.target = target; }
        
        [SerializeField] Vector3Event Output = default;
        
        Vector3 targetVector { get { return target.position - transform.position; } }
        Vector3 targetDirection { get { return targetVector.normalized; } }

        void Update()
        {
            if (!target) return;
            Output.Invoke(targetDirection);
        }
    }
}