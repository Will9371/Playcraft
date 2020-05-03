using UnityEngine;

// REFACTOR: Generalize to follow along other (static) directions
// Dynamic direction is beyond the scope of this component
public class FollowForward : MonoBehaviour
{
    Transform target;
    public void SetTarget(Transform target) { this.target = target; }
    public void ClearTarget() { target = null; }
    
    [SerializeField] RangeCheck followRange;
    [SerializeField] Vector3Event OnMove;
    
    float targetDistance { get { return Vector3.Distance(transform.position, target.position); } }
    
    bool inRange;
    
    void Update()
    {
        if (!target) return;
        
        inRange = followRange.InRange(targetDistance, inRange);
        
        if (inRange)
            OnMove.Invoke(Vector3.forward);
    }
}
