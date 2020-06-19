using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Character/Move State Machine/Standing")]
    public class StandingMoveStateLogic : MoveStateLogic
    {
        #pragma warning disable 0649
        [SerializeField] MoveState idleState;
        [SerializeField] MoveState walkState;
        [SerializeField] MoveState runState;
        [SerializeField] MoveState jumpState;
        #pragma warning restore 0649

        public override MoveState GetState(HumanoidMoveStateMachine self, Vector3 moveInput)
        {
            MoveState state;
        
            if (self.isJumping)
                state = jumpState;
            else if (moveInput == Vector3.zero)
                state = idleState;
            else if (self.isRunning)
                state = runState;
            else
                state = walkState;
                
            return state;
        }
        
        public override bool CanPerformAction(MoveAction action)
        {
            return true;
        }
    }
}