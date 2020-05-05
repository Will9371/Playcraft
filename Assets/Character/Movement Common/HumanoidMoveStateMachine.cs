using System;
using UnityEngine;
using UnityEngine.Events;

public class HumanoidMoveStateMachine : MonoBehaviour
{
    [SerializeField] MoveState defaultState;
    [SerializeField] MoveState idleState, walkState, runState;
    MoveState priorState;
    MoveState state;
    public bool isRunning;
    
    [Serializable] class MoveStateEvent : UnityEvent<MoveState> { }
    [SerializeField] MoveStateEvent BroadcastState;
    
    private void Start()
    {
        state = defaultState;
        priorState = defaultState;
        BroadcastState.Invoke(defaultState);
    }
    
    public void ReceiveInput(Vector3 moveInput)
    {
        if (moveInput == Vector3.zero)
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
    
    public void SetRunning(bool isRunning)
    {
        this.isRunning = isRunning;
    }
}
