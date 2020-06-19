using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Character/Move State Machine/Crouching")]
    public class CrouchingMoveStateLogic : MoveStateLogic
    {
        #pragma warning disable 0649
        [SerializeField] MoveState idleState;
        [SerializeField] MoveState walkState;
        #pragma warning restore 0649

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
