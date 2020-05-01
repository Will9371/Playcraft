using UnityEngine;

public class MovePhysics : MonoBehaviour
{
    Rigidbody rb;
    MoveState state;
    [SerializeField] Vector3Event OnMove;
    
    new bool enabled;
                
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
            Debug.LogError("Attach a Rigidbody!");
    }
    
    public void SetState(MoveState state)
    {
        this.state = state;
    }
    
    public void Enable(bool enabled)
    {
        this.enabled = enabled;
    }
    
    public void Move(Vector3 direction)
    {
        if (!enabled)
            return;
    
        var velocity = direction * state.moveSpeed;
        var step = velocity * Time.deltaTime;
        var nextPosition = transform.position + transform.TransformVector(step);
        rb.MovePosition(nextPosition);
        OnMove.Invoke(transform.TransformVector(velocity));
    }
}
