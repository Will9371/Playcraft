using System;
using UnityEngine;
using UnityEngine.Events;

public class MoveController : MonoBehaviour
{
    MoveData moveData;
    [Serializable] class MoveDataEvent : UnityEvent<MoveData> { }
    [SerializeField] MoveDataEvent BroadcastMoveData;
    
    // Parameters
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    public float rotationSpeed;
    public float jumpStrength;
    
    // Cached variables
    Vector3 moveVector;
    Vector3 moveStep;
    Vector3 rotationAxis;    
    float moveSpeed;
        
    private void Awake()
    {
        moveData = new MoveData(this);
        BroadcastMoveData.Invoke(moveData);
    }
    
    private void Start()
    {
        moveSpeed = walkSpeed;
    }
    
    public void AddMovement(Vector3SO direction)
    {
        AddMovement(direction.value);
    }
    
    public void AddMovement(Vector3 direction)
    {
        moveVector += direction;
    }
    
    public void AddRotation(TurnDirection turn)
    {
        AddRotation(turn.axis, turn.clockwise);
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
        moveData.velocity = moveVector * moveSpeed; 
        moveVector = Vector3.zero; 
    }
    
    private void Rotate()
    {
        rotationAxis = rotationAxis.normalized;
        var rotationStep = rotationAxis.magnitude * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis, rotationStep);
        moveData.rotation = rotationAxis.y;
        rotationAxis = Vector3.zero; 
    }
    
    public void Jump()
    {            
        moveData.beginJumpFlag = true;
    }
    
    public void ToggleSpeed()
    {
        moveSpeed = moveSpeed == walkSpeed ? runSpeed : walkSpeed;
    }
}

public class MoveData
{
    readonly Transform transform;
    public float jumpStrength;

    public MoveData(MoveController system)
    {
        transform = system.transform;
        jumpStrength = system.jumpStrength;
    }
    
    public Vector3 velocity;
    public float rotation;
    public bool beginJumpFlag;
    public bool grounded;
    
    public Vector3 direction { get { return velocity.normalized; } }
    public float speed { get { return velocity.magnitude; } }
    public Vector3 step { get { return velocity * Time.deltaTime; } }
    public Vector3 nextPosition { get { return transform.position + transform.TransformVector(step); } }
    public Vector3 worldVelocity { get { return transform.TransformVector(velocity); } }
        
}
