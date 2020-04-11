using UnityEngine;

public class RigidbodyMovement : MonoBehaviour, IMove, IJump
{
    Rigidbody rb;
    
    [SerializeField] [Range(0f, 1f)] float horizontalJumpDamper = 0.5f;
        
    bool grounded;
    Vector3 step;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
            Debug.LogError("Attach a Rigidbody!");
    }
    
    public void Step(Vector3 step)
    {
        if (!grounded)
            return;
            
        step = transform.TransformDirection(step);
        rb.MovePosition(transform.position + step);
        this.step = step;
    }
    
    public void Jump(Vector3 verticalForce)
    {
        grounded = false;
        var horizontalVelocity = step * horizontalJumpDamper / Time.fixedDeltaTime;
        rb.AddForce(verticalForce + horizontalVelocity, ForceMode.VelocityChange);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        grounded = true;
    }
}
