using UnityEngine;

public class HumanoidMoveStateMachine : MonoBehaviour
{
    MoveData movement;
    InputData input;

    [SerializeField] MoveState idleState, walkState, runState;
    MoveState state;
    
    public void SetMoveData(MoveData movement) { this.movement = movement; }
    public void SetInputData(InputData input) { this.input = input; }

    private void Update()
    {    
        if (input.movement == Vector3.zero)
            state = idleState;
        else if (input.runFlag)
            state = runState;
        else
            state = walkState;
            
        movement.state = state;
    }
}
