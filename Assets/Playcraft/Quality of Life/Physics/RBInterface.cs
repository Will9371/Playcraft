using UnityEngine;

public class RBInterface : MonoBehaviour
{
    new Rigidbody rigidbody;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void SetPhysicsActive(bool active)
    {
        rigidbody.useGravity = active;
        rigidbody.isKinematic = !active;
        
        if (!active)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
    
    public void SetVelocity(Vector3 velocity, Vector3 angularVelocity)
    {
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
    }
}
