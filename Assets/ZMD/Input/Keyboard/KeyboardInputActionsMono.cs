using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    /// Like KeyboardInput, but maps bindings to SO-based IDs (necessary for remappable input)
    public class KeyboardInputActionsMono : MonoBehaviour
    {
        [SerializeField] KeyboardInputActions process;
        [SerializeField] Binding[] bindings;
        
        void Update() 
        {
            process.Update();
            foreach (var value in process.activeValues)
                foreach (var binding in bindings)
                    if (binding.id == value)
                        binding.response.Invoke();
        }
    }
    
    [Serializable]
    public struct Binding
    {
        public SO id;
        public UnityEvent response;
    }
}