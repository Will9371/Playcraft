using UnityEngine;
using UnityEngine.AI;

public class GetSteeringDirection : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    public void SetTarget(Transform value) { target = value; }
    [SerializeField] float pathRefreshRate = 0.5f;
    [SerializeField] float minDistance;
    [SerializeField] Vector3Event Output;
    
    private void Start()
    {
        InvokeRepeating("CalculatePath", 0f, pathRefreshRate);
    }
    
    private void CalculatePath()
    {
        if (!target) return;
        agent.SetDestination(target.position);
    }
    
    private void Update()
    {
        if (!target) return;
        var atTarget = Vector3.Distance(transform.position, target.position) <= minDistance;
        
        if (!atTarget)
        {
            var direction = (agent.steeringTarget - transform.position).normalized;
            Output.Invoke(direction);
        }
    }
}
