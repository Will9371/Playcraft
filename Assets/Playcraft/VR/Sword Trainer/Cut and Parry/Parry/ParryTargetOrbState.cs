using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.SwordTrainer
{
    public class ParryTargetOrbState : MonoBehaviour, IBool
    {
        [SerializeField] MultiCondition fullParryCondition;
        [SerializeField] Binding[] bindings;
        [SerializeField] LerpPositionOverTime extension;

        public float extendTime => extension.duration;
        public bool State => isSuccess;
        
        public enum ParryTargetOrbStateId { Transition, Ready, Success }
        public ParryTargetOrbStateId state;
        bool isSuccess => state == ParryTargetOrbStateId.Success;

        bool swordTouching;
        public void SetSwordTouching(bool value) { swordTouching = value; Refresh(); }
        
        bool readyToParry;
        public void SetReadyToParry(bool value) { readyToParry = value; Refresh(); }
        
        void Refresh()
        {
            var binding = GetBinding();
            binding.Response.Invoke();
            state = binding.id;
            fullParryCondition.Refresh();
        }
        
        public void SetExtended(bool value) 
        {
            if (!extension) return; 
            extension.MoveIfNewDirection(value); 
        }
        
        Binding GetBinding()
        {
            if (!readyToParry) return GetBinding(ParryTargetOrbStateId.Transition);
            if (swordTouching) return GetBinding(ParryTargetOrbStateId.Success);
            return GetBinding(ParryTargetOrbStateId.Ready);
        }
        
        Binding GetBinding(ParryTargetOrbStateId id)
        {
            foreach (var binding in bindings)
                if (binding.id == id)
                    return binding;
            
            Debug.LogError("Invalid binding " + id);        
            return bindings[0];
        }
        
        [Serializable] public struct Binding
        {
            public ParryTargetOrbStateId id;
            public UnityEvent Response;
        }
    }
}
