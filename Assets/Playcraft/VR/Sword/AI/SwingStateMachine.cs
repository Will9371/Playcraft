using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.SwordTrainer
{
    [Serializable] public class SwingStateEvent : UnityEvent<SwingState> { }

    public class SwingStateMachine : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] RotateTowards swingPivot;
        [SerializeField] SwingState startingState;
        [SerializeField] SwingStateEvent PriorState;
        [SerializeField] SwingStateEvent State;
        [SerializeField] float stoppingAngle = 5f;
        #pragma warning restore 0649
        
        SwingState state;
        
        void Start()
        {
            state = startingState;
            Swing();
        }
        
        void Update()
        {
            if (swingPivot.angle < stoppingAngle)
                Swing();
        }

        void Swing()
        {
            if (state) PriorState.Invoke(state);
            state = state.GetRandom();
            swingPivot.SetDirection(state.direction.value);
            State.Invoke(state);
        }
    }
}
