using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody rb; //Leave empty and attach a controller for non-rigidbody movement
    [SerializeField] RaycastController control; 
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    Vector3 moveStep;
    Vector3 rotationAxis;

    //For non-rb physics
    [SerializeField] Vector3 gravity = Vector3.zero;

    public void AddMovement(Vector3 direction)
    {
        moveStep += direction;
    }
    
    public void AddRotation(Axis axis, bool clockwise)
    {            
        rotationAxis += StaticAxis.GetAxisVector(axis, clockwise);
    }
    
    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    
    private void Move()
    {
        moveStep = moveStep.normalized * movementSpeed * Time.deltaTime;

        if (rb)
        {
            moveStep = transform.TransformDirection(moveStep);
            rb.MovePosition(transform.position + moveStep);
        }
        else if (control != null)
        {
            Vector3 velocity = moveStep;
            velocity += gravity * Time.deltaTime;
            control.Move(velocity);
        }
        else Debug.LogError("Movement requires Rigidbody or Controller");

        moveStep = Vector3.zero;          
    }
    
    private void Rotate()
    {
        rotationAxis = rotationAxis.normalized;
        var rotationStep = rotationAxis.magnitude * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis, rotationStep);
        rotationAxis = Vector3.zero; 
    }
}
