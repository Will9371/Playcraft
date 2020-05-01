using UnityEngine;

// Controls an AI instance
public class AI : MonoBehaviour
{
    // Dependencies
    [HideInInspector] public InputMovement movement;

    // Properties
    public Transform target;
    public AIState state;
    
    // Calculated
    public Vector3 position { get { return transform.position; } }
    public Vector3 forward { get { return transform.forward; } }
    public Vector3 targetVector { get { return target == null ? Vector3.zero : target.position - transform.position; } }
    public Vector3 targetDirection { get { return targetVector.normalized; } }
    public float targetDistance { get { return targetVector.magnitude; } }
    
    public bool inRangeOfTarget;
    
    void Awake()
    {
        movement = GetComponent<InputMovement>();
        
        if (!movement)
            Debug.Log("AI requires a MoveController!");
    }
    
    void Update()
    {
        state.Tick(this);
    }
    
    public void MoveToTarget()
    {
        var direction = state.faceTarget ? Vector3.forward : targetDirection;
        movement.AddMovement(direction); 
    }
        
    public void TurnToTarget()
    {          
        var targetDelta = StaticAxis.AngleAroundAxis(forward, targetVector, Vector3.up);
        var maxDelta = (targetDelta > 0f ? 1f : -1f) * movement.rotationSpeed * Time.deltaTime;
        var overshoot = Mathf.Abs(maxDelta) > Mathf.Abs(targetDelta);
        
        if (!overshoot)
            movement.AddRotation(Axis.Y, targetDelta > 0f);
    }
}
