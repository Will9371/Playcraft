using System;
using UnityEngine;
using UnityEngine.Events;

public class MoveController : MonoBehaviour
{
    MoveData data;
    [SerializeField] MoveState idleState, walkState, runState;
    [Serializable] class MoveDataEvent : UnityEvent<MoveData> { }
    [SerializeField] MoveDataEvent BroadcastMoveData;
        
    // Parameters
    public float rotationSpeed;
    public float jumpStrength;
    
    // Cached variables (from input)
    Vector3 moveInput;
    Vector3 moveStep;
    Vector3 rotationAxis;
    bool runFlag;
        
    private void Awake()
    {
        data = new MoveData(this);
        data.state = GetState();
        BroadcastMoveData.Invoke(data);
    }
    
    public void AddMovement(Vector3SO direction)
    {
        AddMovement(direction.value);
    }
    
    public void AddMovement(Vector3 direction)
    {
        moveInput += direction;
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
        moveInput = moveInput.normalized;
        data.state = GetState();
        
        data.velocity = moveInput * data.state.moveSpeed; 
        moveInput = Vector3.zero; 
    }
    
    private void Rotate()
    {
        rotationAxis = rotationAxis.normalized;
        var rotationStep = rotationAxis.magnitude * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis, rotationStep);
        data.rotation = rotationAxis.y;
        rotationAxis = Vector3.zero; 
    }
    
    public void Jump()
    {            
        data.OnRequestJump(jumpStrength);
    }
    
    public void SetRun(bool active)
    {
        runFlag = active;
    }
    
    // DELEGATE to SO so different systems can use different state machines
    private MoveState GetState()
    {
        if (moveInput == Vector3.zero)
            return idleState;
        if (runFlag)
            return runState;

        return data.state = walkState;
    }
}

public class MoveData
{
    readonly Transform transform;

    public MoveData(MoveController system)
    {
        transform = system.transform;
    }
    
    public MoveState state;
    
    public Vector3 velocity;
    public float rotation;
    
    public Action<float> OnRequestJump;
    public bool grounded;
    
    public Vector3 direction { get { return velocity.normalized; } }
    public float speed { get { return velocity.magnitude; } }
    public Vector3 step { get { return velocity * Time.deltaTime; } }
    public Vector3 nextPosition { get { return transform.position + transform.TransformVector(step); } }
    public Vector3 worldVelocity { get { return transform.TransformVector(velocity); } }    
}
