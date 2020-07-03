using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocalStateListener : MonoBehaviour
{
    [SerializeField] LocalStateHub stateHub;
    [SerializeField] StateCondition[] conditions;
    
    private void OnEnable()
    {
        stateHub.OnStateEnter += RespondToState;
    }
    
    private void OnDisable()
    {
        stateHub.OnStateEnter -= RespondToState;
    }
        
    public void RespondToState(TagSO value) 
    {            
        foreach (var item in conditions)
            if (item.state == value)
                item.OnEnter.Invoke();
    }

    [Serializable] struct StateCondition
    {
        public TagSO state;
        public UnityEvent OnEnter;
    }
}
