using UnityEngine;

public class RigidbodyMovement : MonoBehaviour, IMove, IJump
{
    Rigidbody rb;
    
    [SerializeField] [Range(0f, 1f)] float horizontalJumpDamper = 1f;
        
    bool grounded;
    MoveData data;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
            Debug.LogError("Attach a Rigidbody!");
    }
    
    public void Tick(MoveData data)
    {
        if (!grounded)
            return;

        this.data = data;
        rb.MovePosition(data.WorldStep);
    }
    
    public void Jump(Vector3 verticalForce)
    {
        if (!grounded)
            return;
    
        grounded = false;
        var horizontalVelocity = data.WorldVelocity * horizontalJumpDamper;
        rb.AddForce(verticalForce + horizontalVelocity, ForceMode.VelocityChange);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        grounded = true;
    }
}
