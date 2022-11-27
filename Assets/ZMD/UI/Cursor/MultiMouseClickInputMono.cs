using System;
using UnityEngine;
using UnityEngine.Events;

// Input: Game Engine
// Process: continuous check for mouse button clicks
// Output: event triggers per type
// For interactions with specific objects (UI or collider), use RespondToMouse
namespace ZMD
{
    public class MultiMouseClickInputMono : MonoBehaviour
    {
        public Binding[] bindings;
        
        void Update()
        {
            foreach (var binding in bindings)
                binding.Update();
        }
        
        [Serializable]
        public class Binding
        {
            [SerializeField] MouseClickInput input;
            [SerializeField] UnityEvent output;
            public void Update() { if (input.Update()) output.Invoke(); }        
        }
    }
}
