using System;
using UnityEngine;

namespace Playcraft.AI
{
    public class AIStateMachine : MonoBehaviour
    {
        [SerializeField] AIState startingState;
        [SerializeField] LocalStateHub stateBroadcast;

        AIState state;
        Collider target;
        
        [NonSerialized] public float enterStateTime;
        [NonSerialized] public bool canSeeTarget;
        
        void Start() { state = startingState; }

        public void SetTarget(Collider other)
        {
            target = other;
            canSeeTarget = target != null;
            CheckExitConditions();
        }
        
        void CheckExitConditions() { state.CheckExitConditions(this); }
        
        public void SetState(AIState value) 
        {
            stateBroadcast.SetState(value);
            state = value; 
            enterStateTime = Time.time;
        }
    }
}
