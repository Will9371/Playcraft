using UnityEngine;

public class NonRigidbodyMovement : MonoBehaviour
{
    public Vector3 gravity;
    [SerializeField] float horizontalVelocitySmoothing = 0.1f; //Time
    [SerializeField] float maxSpeed = 1f;
    Vector3 velocitySmoothStorage;
    Vector3 inputVelocity;
    public Vector3 velocity = Vector3.zero;
    RaycastController raycastController;
        
    private void Awake()
    {
        raycastController = GetComponent<RaycastController>();
        
        if (raycastController == null)
            Debug.LogError("Attach a RaycastController!");
    }
    
    public void SetInputVelocity(Vector3 inputVelocity)
    {
        this.inputVelocity = transform.TransformDirection(inputVelocity);
    }
    
    private void FixedUpdate()
    {
        if (raycastController.collisions.above || raycastController.collisions.below)
            velocity -= Vector3.Dot(gravity.normalized, velocity) * gravity.normalized;

        //Apply gravity
        velocity += gravity * Time.deltaTime;

        //Apply horizontal velocity with smoothing
        Vector3 verticalVelocity = gravity.normalized * Vector3.Dot(gravity.normalized, velocity);
        Vector3 currentHorizontalVelocity = velocity - verticalVelocity;
        velocity = Vector3.SmoothDamp(currentHorizontalVelocity, inputVelocity, ref velocitySmoothStorage, horizontalVelocitySmoothing, maxSpeed) + verticalVelocity;

        raycastController.ApplyCollisions(ref velocity, gravity);
        transform.Translate(velocity, Space.World);
        velocity = gravity.normalized * Vector3.Dot(gravity.normalized, velocity); //Maintain falling velocity
    }
}
