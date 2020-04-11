using UnityEngine;

public class NonRigidbodyMovement : MonoBehaviour, IMove
{
    [SerializeField] Vector3 gravity;
    RaycastController raycastController;
    
    private void Awake()
    {
        raycastController = GetComponent<RaycastController>();
        
        if (raycastController == null)
            Debug.LogError("Attach a RaycastController!");
    }

    public void Step(Vector3 velocity)
    {
        velocity += gravity * Time.deltaTime;
        raycastController.ApplyCollisions(ref velocity);
        transform.Translate(velocity);        
    }
}
