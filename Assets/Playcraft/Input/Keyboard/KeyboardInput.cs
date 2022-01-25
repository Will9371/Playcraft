using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    /// Simplistic approach to getting keyboard input, useful for prototyping
    /// Use KeyboardInputActions(Mono) for remappble, action-based input
    public class KeyboardInput : MonoBehaviour
    {    
        [SerializeField] Binding[] keyBindings;

        void Update()
        {
            foreach (var binding in keyBindings)
                binding.Update();
        }
        
        [Serializable]
        public class Binding
        {
            [SerializeField] Keybinding input;
            [SerializeField] UnityEvent response;
            
            public void Update()
            {
                if (input.IsActive())
                    response.Invoke();
            }
        }
    }
}