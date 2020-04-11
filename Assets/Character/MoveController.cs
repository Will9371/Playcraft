using UnityEngine;

public class MoveController : MonoBehaviour
{
    Movement moveSystem;
    
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    Vector3 moveVector;
    Vector3 priorMoveVector;
    Vector3 moveStep;
    Vector3 rotationAxis;    
    float currentMoveSpeed;
    
    [SerializeField] FloatEvent BroadcastMoveSpeed;
    
    private void Awake()
    {
        moveSystem = GetComponent<Movement>();
        
        if (moveSystem == null)
            Debug.LogError("Must attach a move system component (RigidbodyMovement or NonRigidbodyMovement)");
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
        moveStep = moveVector * Time.deltaTime;
        
        if (moveVector != priorMoveVector)
            BroadcastMoveSpeed.Invoke(moveVector.magnitude * movementSpeed);         
         
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
}

public abstract class Movement : MonoBehaviour
{
    public abstract void Step(Vector3 step);
}
