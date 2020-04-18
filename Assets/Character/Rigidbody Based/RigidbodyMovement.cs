using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    Rigidbody rb;
    MoveData data;
    
    [SerializeField] [Range(0f, 1f)] float jumpHorizontalDamper;
            
    public bool grounded;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
            Debug.LogError("Attach a Rigidbody!");
    }
    
    public void SetMoveData(MoveData data)
    {
        this.data = data;
    }
    
    private void Update()
    {
        if (!grounded)
            return;

        rb.MovePosition(data.nextPosition);
        
        if (data.beginJumpFlag)
        {
            data.beginJumpFlag = false;
            Jump(data.jumpStrength);
        }
    }
    
    private void Jump(float verticalForce)
    {
        if (!grounded)
            return;
    
        grounded = false;
        
        var vertical = Vector3.up * verticalForce;
        var horizontal = data.worldVelocity * jumpHorizontalDamper;
        rb.velocity = vertical + horizontal;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        grounded = true;
    }
}
