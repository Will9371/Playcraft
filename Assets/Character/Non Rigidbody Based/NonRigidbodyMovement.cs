using UnityEngine;

public class NonRigidbodyMovement : MonoBehaviour
{
    [SerializeField] Vector3 gravity;
    [SerializeField] float horizontalVelocitySmoothing = 0.1f; //Time
    [SerializeField] float maxSpeed = 1f;
    Vector3 velocitySmoothStorage;
    public Vector3 velocity = Vector3.zero;
    RaycastController raycastController;
    MoveData data;
    
    private void Awake()
    {
        raycastController = GetComponent<RaycastController>();
        
        if (raycastController == null)
            Debug.LogError("Attach a RaycastController!");
    }
    
    public void SetMoveData(MoveData data)
    {
        this.data = data;
    }
    
    private void Update()
    {
        if (raycastController.collisions.above || raycastController.collisions.below)
            velocity.y = 0;

        //Apply gravity
        velocity += gravity * Time.deltaTime;

        //Apply horizontal velocity with smoothing
        Vector3 verticalVelocity = gravity.normalized * Vector3.Dot(gravity.normalized, velocity);
        Vector3 currentHorizontalVelocity = velocity - verticalVelocity;
        velocity = Vector3.SmoothDamp(currentHorizontalVelocity, data.velocity, ref velocitySmoothStorage, horizontalVelocitySmoothing, maxSpeed) + verticalVelocity;
        
        raycastController.ApplyCollisions(ref velocity);
        transform.Translate(velocity);
        velocity = gravity.normalized * Vector3.Dot(gravity.normalized, velocity); //Maintain falling velocity
    }
}
