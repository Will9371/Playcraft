﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    /// Instanced SO-based state machine
    public class LocalStateListener : MonoBehaviour
    {
        [SerializeField] LocalStateHub stateHub;
        [SerializeField] StateCondition[] conditions;
        
        void Awake() { if (!stateHub) Debug.LogError("State listener requires hub", gameObject); }
        
        void OnEnable() { stateHub.OnStateEnter += RespondToState; }
        void OnDisable() { stateHub.OnStateEnter -= RespondToState; }
            
        public void RespondToState(SO value) 
        {
            //Debug.Log(gameObject + " responding to " + value);            
            foreach (var item in conditions)
                if (item.state == value)
                    item.OnEnter.Invoke();
        }

        [Serializable] 
        struct StateCondition
        {
            public SO state;
            public UnityEvent OnEnter;
        }
    }
}