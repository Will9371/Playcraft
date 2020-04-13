using UnityEngine;

public class NonRigidbodyMovement : MonoBehaviour, IMove
{
    [SerializeField] Vector3 gravity;
    [SerializeField] float horizontalVelocitySmoothing = 0.25f; //Time
    [SerializeField] float maxSpeed = 7f;
    Vector3 velocitySmoothStorage;
    Vector3 velocity = Vector3.zero;
    RaycastController raycastController;
    
    private void Awake()
    {
        raycastController = GetComponent<RaycastController>();
        
        if (raycastController == null)
            Debug.LogError("Attach a RaycastController!");
    }

    public void Step(Vector3 step)
    {
        if (raycastController.collisions.above || raycastController.collisions.below)
            velocity.y = 0;

        //Apply gravity
        velocity += gravity * Time.deltaTime;
        velocity += step;
        
        raycastController.ApplyCollisions(ref velocity);
        transform.Translate(velocity);
        velocity = gravity.normalized * Vector3.Dot(gravity.normalized, velocity); //Maintain falling velocity
    }
}
