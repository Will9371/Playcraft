using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class LocalStateListener : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] LocalStateHub stateHub;
        [SerializeField] StateCondition[] conditions;
        #pragma warning restore 0649
        
        private void OnEnable()
        {
            stateHub.OnStateEnter += RespondToState;
        }
        
        private void OnDisable()
        {
            stateHub.OnStateEnter -= RespondToState;
        }
            
        public void RespondToState(SO value) 
        {
            //Debug.Log(gameObject + " responding to " + value);            
            foreach (var item in conditions)
                if (item.state == value)
                    item.OnEnter.Invoke();
        }

        [Serializable] struct StateCondition
        {
            #pragma warning disable 0649
            public SO state;
            public UnityEvent OnEnter;
            #pragma warning restore 0649
        }
    }
}
