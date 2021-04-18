using UnityEngine;

public class MaintainDistance : MonoBehaviour
{
    [SerializeField] Transform self;
    [SerializeField] Vector2 desiredDistance;
    [SerializeField] float tolerance;
    [SerializeField] Transform target;
    [SerializeField] bool disableVerticalMovement;
    [SerializeField] Vector3Event Output;
    
    float targetDistance => Vector3.Distance(self.position, target.position);
    Vector3 targetDirection => (target.position - self.position).normalized;
    
    float moveTowards;
    Vector3 moveDirection;
    
    public void SetTarget(Transform value) { target = value; }
    
    public void SetDesiredDistance(float value)
    {
        desiredDistance = new Vector2(value - tolerance, value + tolerance);
    }
    
    void Start() { if (!self) self = transform; }
    
    void Update()
    {
        if (!target) return;
        
        if (targetDistance > desiredDistance.y)
            moveTowards = 1f;
        else if (targetDistance < desiredDistance.x)
            moveTowards = -1f;
        else
            moveTowards = 0f;
        
        if (moveTowards != 0f)
        {
            moveDirection = targetDirection;
            
            if (disableVerticalMovement)
                moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
            
            Output.Invoke(moveTowards * moveDirection);
        }
    }
}
