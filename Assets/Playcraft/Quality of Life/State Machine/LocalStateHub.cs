using System;
using UnityEngine;

namespace Playcraft
{
    public class LocalStateHub : MonoBehaviour
    {
        SO priorState;
        public SO state;    // For debug
        public Action<SO> OnStateEnter;
        
        public void SetState(SO value)
        {
            priorState = state == null ? value : state;
            state = value;
            OnStateEnter.Invoke(value);
        }
        
        public void ReturnToPriorState()
        {
            SetState(priorState);
        }
    }
}
