using UnityEngine;

public class RigidbodyMovement : MonoBehaviour, IMove
{
    Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
            Debug.LogError("Attach a Rigidbody!");
    }
    
    public void Step(Vector3 step)
    {
        step = transform.TransformDirection(step);
        rb.MovePosition(transform.position + step);
    }
}
