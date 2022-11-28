﻿using System;
using UnityEngine;

namespace ZMD
{
    public class LocalStateHub : MonoBehaviour
    {
        SO priorState;
        [ReadOnly] public SO state;
        public Action<SO> OnStateEnter;
        
        public void SetState(SO value)
        {
            priorState = state == null ? value : state;
            state = value;
            OnStateEnter?.Invoke(value);
        }
        
        public void ReturnToPriorState() { SetState(priorState); }
    }
}
