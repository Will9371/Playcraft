using System;
using UnityEngine;

namespace Playcraft.AI
{
    public class AIStateMachine : MonoBehaviour
    {
        [SerializeField] AIState startingState;
        [SerializeField] LocalStateHub stateBroadcast;

        [Header("Visible for debug")]
        public AIState state;
        public Collider target;
        public float targetDistance => target ? Vector3.Distance(target.transform.position, transform.position) : 0;
        
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
