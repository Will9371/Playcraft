using UnityEngine;
using UnityEngine.Events;

public class JumpPhysics : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    
    [SerializeField] float jumpStrength;
    [SerializeField] [Range(0f, 1f)] float jumpHorizontalDamper;
    
    bool grounded;
    Vector3 velocity;
    
    [SerializeField] UnityEvent OnJump;
    [SerializeField] UnityEvent OnLand;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();        
        if (rb == null)
            Debug.LogError("Attach a Rigidbody!");
    }
    
    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }
    
    public void Jump()
    {
        //Debug.Log("Jump method reached " + grounded);
        if (!grounded)
            return;
    
        grounded = false;
        
        var vertical = Vector3.up * jumpStrength;
        var horizontal = velocity * jumpHorizontalDamper;
        //Debug.Log(vertical + " " + horizontal);
        rb.velocity = vertical + horizontal;
        OnJump.Invoke();
    }
    
    public void Land()
    {
        grounded = true;
        OnLand.Invoke();
    }
}
