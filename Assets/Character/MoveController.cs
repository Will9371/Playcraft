using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Dependencies
    IMove moveSystem;
    IJump jumpSystem;
    MoveData moveData;
    
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
        moveData = new MoveData(moveSystem, transform);
        
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
        if (moveVector != priorMoveVector)
            BroadcastMoveSpeed.Invoke(moveVector.magnitude);     
         
        moveData.Tick(moveVector.normalized, movementSpeed);

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
        jumpSystem?.Jump(Vector3.up * jumpStrength);
    }
}

public interface IMove { void Tick(MoveData data); }

public interface IJump { void Jump(Vector3 vector); }

public class MoveData
{
    IMove system;
    Transform transform;

    public MoveData(IMove system, Transform transform)
    {
        this.system = system;
        this.transform = transform;
    }

    public Vector3 direction;
    public float speed;
    
    public void Tick(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        
        system.Tick(this);
    }
    
    public Vector3 velocity { get { return direction * speed; } }
    public Vector3 step { get { return velocity * Time.deltaTime; } }
    public Vector3 WorldStep { get { return transform.position + transform.TransformDirection(step); } }
    public Vector3 WorldVelocity { get { return transform.TransformDirection(velocity); } }
}
