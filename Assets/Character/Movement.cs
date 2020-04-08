using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody rb; //Leave empty and attach a controller for non-rigidbody movement
    [SerializeField] RaycastController control; 
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    Vector3 moveVector;
    Vector3 priorMoveVector;
    Vector3 moveStep;
    Vector3 rotationAxis;

    //For non-rb physics
    [SerializeField] Vector3 nonRBGravity = Vector3.zero;
    
    float currentMoveSpeed;
    
    [SerializeField] FloatEvent BroadcastMoveSpeed;
    
    public void AddMovement(Vector3 direction)
    {
        moveVector += direction;
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
        moveVector = moveVector.normalized * movementSpeed;
        moveStep = moveVector * Time.deltaTime;
        
        if (moveVector != priorMoveVector)
            BroadcastMoveSpeed.Invoke(moveVector.magnitude * movementSpeed);         
        
       // NOT EXTENSIBLE: delegate to interface if this logic becomes more complex (and both branches needed)
       if (rb)
        {
            moveStep = transform.TransformDirection(moveStep);
            rb.MovePosition(transform.position + moveStep);
        }
        else if (control != null)
        {
            Vector3 velocity = moveStep;
            velocity += nonRBGravity * Time.deltaTime;
            control.Move(velocity);
        }
        else Debug.LogError("Movement requires Rigidbody or Controller");

        priorMoveVector = moveVector;
        moveVector = Vector3.zero; 
    }
    
    private void Rotate()
    {
        rotationAxis = rotationAxis.normalized;
        var rotationStep = rotationAxis.magnitude * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis, rotationStep);
        rotationAxis = Vector3.zero; 
    }
}
