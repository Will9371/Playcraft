using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class HumanoidMoveStateMachine : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] MoveStateLogic standingStateLogic;
        [SerializeField] MoveStateLogic crouchingStateLogic;
       
        [Serializable] class MoveStateEvent : UnityEvent<MoveState> { }
        [SerializeField] MoveStateEvent BroadcastState;
         #pragma warning restore 0649
            
        MoveStateLogic stateLogic;
        MoveState state;
        MoveState priorState;
        
        public bool isRunning;
        public bool isJumping;
        public bool isCrouching;
        
        private void Start()
        {
            stateLogic = GetStateLogic(isCrouching);
            state = stateLogic.GetState(this, Vector3.zero);
            BroadcastState.Invoke(state);
            priorState = state;
        }
        
        private MoveStateLogic GetStateLogic(bool isCrouching)
        {
            return isCrouching ? crouchingStateLogic : standingStateLogic;
        }
        
        public void ReceiveInput(Vector3 moveInput)
        {
            stateLogic = GetStateLogic(isCrouching);
            SetState(stateLogic.GetState(this, moveInput));
        }
        
        public void SetState(MoveState state)
        {
            this.state = state;
        
            if (state == priorState)
                return;
                
            BroadcastState.Invoke(state);
            priorState = state;        
        }
        
        public void ToggleRunning() { SetRunning(!isRunning); }
        public void SetRunning(bool isRunning) 
        { if (stateLogic.CanPerformAction(MoveAction.Run)) this.isRunning = isRunning; }
        
        public void ToggleCrouching() { SetCrouching(!isCrouching); }
        public void SetCrouching(bool isCrouching) { this.isCrouching = isCrouching; }
        
        public void Jump() { if (stateLogic.CanPerformAction(MoveAction.Jump)) isJumping = true; }
        public void Land() { isJumping = false; }
    }
}
