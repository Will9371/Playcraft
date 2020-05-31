using UnityEngine;

namespace Playcraft
{
    public abstract class MoveStateLogic : ScriptableObject
    {
        public abstract MoveState GetState(HumanoidMoveStateMachine self, Vector3 moveInput);
        public abstract bool CanPerformAction(MoveAction action);
    }

    public enum MoveAction
    {
        Run, 
        Jump
    }
}
