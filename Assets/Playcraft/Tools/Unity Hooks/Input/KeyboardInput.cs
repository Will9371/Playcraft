using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class KeyboardInput : MonoBehaviour
    {    
        #pragma warning disable 0649
        [SerializeField] Keybinding[] bindings;
        #pragma warning restore 0649
        
        void Update()
        {
            foreach (var binding in bindings)
                binding.Update();
        }
    }

    [Serializable]
    public class Keybinding
    {
        #pragma warning disable 0649
        [SerializeField] KeyCode[] keys;
        [SerializeField] PressType pressType;
        [SerializeField] UnityEvent OnActive;
        #pragma warning restore 0649
        
        public void Update()
        {
            bool active = false;
        
            foreach (var key in keys)
            {            
                switch (pressType)
                {
                    case PressType.Down: active = Input.GetKeyDown(key); break;
                    case PressType.Up: active = Input.GetKeyUp(key); break;
                    case PressType.Continuous: active = Input.GetKey(key); break;
                }
                
                if (active) break;
            }
            
            if (active) OnActive.Invoke();
        }
    }    
}