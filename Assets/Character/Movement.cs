using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    Vector3 moveVector;
    Vector3 priorMoveVector;
    Vector3 moveStep;
    Vector3 rotationAxis;
    
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
    
    private void Update()
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
        else
            transform.Translate(moveStep); 

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
