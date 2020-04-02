using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    Vector3 moveStep;
    Vector3 rotationAxis;
    
    public void AddMovement(Vector3 direction)
    {
        moveStep += direction;
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
        moveStep = moveStep.normalized * movementSpeed * Time.deltaTime;
        
        if (rb)
        {
            moveStep = transform.TransformDirection(moveStep);
            rb.MovePosition(transform.position + moveStep);             
        }
        else
            transform.Translate(moveStep); 

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
