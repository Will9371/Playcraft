using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class StateEventListener : GameEventListener
    {
        #pragma warning disable 0649
        [SerializeField] bool debug;
        [SerializeField] StateCondition[] conditions;
        #pragma warning restore 0649
        
        public override void OnEventRaised(SO value) 
        {
            if (debug)
                Debug.Log(gameObject.name + " receiving state " + value.name); 
            
            foreach (var item in conditions)
                if (item.state == value)
                    item.OnEnter.Invoke();
        }
    }
    
    [Serializable] struct StateCondition
    {
        #pragma warning disable 0649
        public SO state;
        public UnityEvent OnEnter;
        #pragma warning restore 0649
    }
}
