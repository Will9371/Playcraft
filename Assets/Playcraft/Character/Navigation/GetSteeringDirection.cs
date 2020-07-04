using UnityEngine;
using UnityEngine.AI;

namespace Playcraft.Navigation
{
    public class GetSteeringDirection : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] NavMeshAgent agent;
        [SerializeField] Transform target;
        [SerializeField] float pathRefreshRate = 0.5f;
        [SerializeField] float minDistance;
        [SerializeField] Vector3Event Output;
        #pragma warning restore 0649
        
        public void SetTarget(Transform value) { target = value; }
        public void SetTargetPosition(RaycastHit value) { SetTargetPosition(value.point); }
        public void SetTargetPosition(Vector3 value) { targetPosition = value; }
        public void ClearTargetPosition() { targetPosition = null; }
        
        Vector3? targetPosition;
        
        private void Start()
        {
            InvokeRepeating("CalculatePath", 0f, pathRefreshRate);
        }
        
        private void CalculatePath()
        {
            if (!target && targetPosition == null) return;
            var destination = target ? target.position : targetPosition;
            agent.SetDestination((Vector3)destination);
        }
        
        private void Update()
        {
            if (!target && targetPosition == null) return;
            var destination = target ? target.position : targetPosition;
            var atTarget = Vector3.Distance(transform.position, (Vector3)destination) <= minDistance;
            
            if (!atTarget)
            {
                var direction = (agent.steeringTarget - transform.position).normalized;
                Output.Invoke(direction);
            }
        }
    }
}
