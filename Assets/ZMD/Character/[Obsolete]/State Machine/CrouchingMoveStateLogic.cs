using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Legacy/Character/Move State Machine/Crouching")]
    public class CrouchingMoveStateLogic : MoveStateLogic
    {
        [SerializeField] MoveState idleState;
        [SerializeField] MoveState walkState;

        public override MoveState GetState(HumanoidMoveStateMachine self, Vector3 moveInput)
        {            
            return moveInput == Vector3.zero ? idleState : walkState;
        }
        
        public override bool CanPerformAction(MoveAction action)
        {
            return false;
        }
    }
}
