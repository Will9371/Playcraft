using UnityEngine;

public class RigidbodyMovement : Movement
{
    Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
            Debug.LogError("Attach a Rigidbody!");
    }
    
    public override void Step(Vector3 step)
    {
        step = transform.TransformDirection(step);
        rb.MovePosition(transform.position + step);
    }
}
