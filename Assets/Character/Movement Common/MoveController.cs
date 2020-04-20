using System;
using UnityEngine;
using UnityEngine.Events;

public class MoveController : MonoBehaviour
{
    [HideInInspector] public MoveData movement;
    [SerializeField] MoveState startingState;
    [Serializable] class MoveDataEvent : UnityEvent<MoveData> { }
    [SerializeField] MoveDataEvent BroadcastMoveData;
    
    [HideInInspector] public InputData input;
    [Serializable] class InputDataEvent : UnityEvent<InputData> { }
    [SerializeField] InputDataEvent BroadcastInputData;
        
    // Parameters
    public float rotationSpeed;
    public float jumpStrength;
    
    // Cached variables (from input)
    [HideInInspector] public Vector3 moveInput;
    Vector3 rotationAxis;
        
    private void Awake()
    {
        movement = new MoveData(this);
        movement.state = startingState;
        BroadcastMoveData.Invoke(movement);
        
        input = new InputData();
        BroadcastInputData.Invoke(input);
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
        input.movement = moveInput;
        movement.velocity = input.movement * movement.state.moveSpeed; 
        moveInput = Vector3.zero; 
    }
    
    private void Rotate()
    {
        rotationAxis = rotationAxis.normalized;
        var rotationStep = rotationAxis.magnitude * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis, rotationStep);
        movement.rotation = rotationAxis.y;
        rotationAxis = Vector3.zero; 
    }
    
    public void Jump()
    {            
        movement.OnRequestJump(jumpStrength);
    }
    
    public void SetRun(bool active)
    {
        input.runFlag = active;
    }
}

public class InputData
{
    public Vector3 movement;
    public bool runFlag;
    
    public void Normalize() { movement = movement.normalized; }
    public void Zero() { movement = Vector3.zero; }
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
