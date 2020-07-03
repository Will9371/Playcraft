using UnityEngine;

public class GetTargetDirection : MonoBehaviour
{
    [SerializeField] Transform target;
    public void SetTarget(Transform value) { target = value; }
    [SerializeField] float minDistance;
    
    [SerializeField] Vector3Event Output;
    
    private void Update()
    {
        if (!target) return;
        var atTarget = Vector3.Distance(transform.position, target.position) <= minDistance;
        
        if (!atTarget)
        {
            var direction = (target.position - transform.position).normalized;
            Output.Invoke(direction);
        }
    }
}
