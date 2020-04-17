using UnityEngine;

// Controls an AI instance
public class AI : MonoBehaviour
{
    // Dependencies
    [HideInInspector] public MoveController movement;

    // Properties
    public Transform target;
    public AIState state;
    
    // Calculated
    public Vector3 position { get { return transform.position; } }
    public Vector3 forward { get { return transform.forward; } }
    public Vector3 targetVector { get { return target == null ? Vector3.zero : target.position - transform.position; } }
    public Vector3 targetDirection { get { return targetVector.normalized; } }
    public float targetDistance { get { return targetVector.magnitude; } }
    
    void Awake()
    {
        movement = GetComponent<MoveController>();
        
        if (!movement)
            Debug.Log("AI requires a MoveController!");
    }
    
    void Update()
    {
        state.Tick(this);
    }
    
    public void MoveToTarget()
    {
        movement.AddMovement(Vector3.forward); 
    }
    
    public float minimumTurnAngle = 5f;    // Remove: prevent overshoot in TurnToTarget
    
    public void TurnToTarget()
    {          
        var angle = StaticAxis.AngleAroundAxis(forward, targetVector, Vector3.up);
        
        if (angle > minimumTurnAngle)
            movement.AddRotation(Axis.Y, true);
        else if (angle < -minimumTurnAngle)
            movement.AddRotation(Axis.Y, false);         
    }
}
