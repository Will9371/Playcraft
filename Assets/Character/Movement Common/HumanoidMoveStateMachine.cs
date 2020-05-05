using System;
using UnityEngine;
using UnityEngine.Events;

public class HumanoidMoveStateMachine : MonoBehaviour
{
    [SerializeField] MoveState defaultState;
    
    [Serializable] class MoveStateEvent : UnityEvent<MoveState> { }
    [SerializeField] MoveStateEvent BroadcastState;
    
    [Header("Movement States")]
    [SerializeField] MoveState idleState;
    [SerializeField] MoveState walkState;
    [SerializeField] MoveState runState;
    [SerializeField] MoveState jumpState;
    
    MoveState priorState;
    MoveState state;
    bool isRunning;
    bool isJumping;
    
    private void Start()
    {
        state = defaultState;
        priorState = defaultState;
        BroadcastState.Invoke(defaultState);
    }
    
    public void ReceiveInput(Vector3 moveInput)
    {
        if (isJumping)
            state = jumpState;
        else if (moveInput == Vector3.zero)
            state = idleState;
        else if (isRunning)
            state = runState;
        else
            state = walkState;
        
        if (state == priorState)
            return;
            
        BroadcastState.Invoke(state);
        priorState = state;
    }
    
    public void SetRunning(bool isRunning) { this.isRunning = isRunning; }
    
    public void Jump() { isJumping = true; }
    public void Land() { isJumping = false; }
}
