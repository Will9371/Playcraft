using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class StateEventListener : GameEventListener
    {
        [SerializeField] bool debug;
        [SerializeField] StateCondition[] conditions;
        
        public override void OnEventRaised(TagSO value) 
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
        public TagSO state;
        public UnityEvent OnEnter;
    }
}
