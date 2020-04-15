using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Dependencies
    IMove moveSystem;
    IJump jumpSystem;
    
    // Parameters
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpStrength;
    
    // Cached variables
    Vector3 moveVector;
    Vector3 priorMoveVector;
    Vector3 moveStep;
    Vector3 rotationAxis;    
    float currentMoveSpeed;
    
    [SerializeField] FloatEvent BroadcastMoveSpeed;
    
    private void Awake()
    {
        moveSystem = GetComponent<IMove>();
        
        if (moveSystem == null)
            Debug.LogError("Must attach a move system component (RigidbodyMovement or NonRigidbodyMovement)");
            
        jumpSystem = GetComponent<IJump>();
    }
    
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
        moveStep = moveVector;
        
        if (moveVector != priorMoveVector)
            BroadcastMoveSpeed.Invoke(moveVector.magnitude);     
         
        moveSystem.Step(moveStep);

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
    
    public void Jump()
    {
        if (jumpSystem == null)
            return;
            
        jumpSystem.Jump(Vector3.up * jumpStrength);
    }
}

public interface IMove 
{
    void Step(Vector3 step);
}

public interface IJump
{
    void Jump(Vector3 vector);
}
