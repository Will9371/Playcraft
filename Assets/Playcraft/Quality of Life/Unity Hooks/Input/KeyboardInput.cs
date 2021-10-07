using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class KeyboardInput : MonoBehaviour
    {    
        [SerializeField] Keybinding[] bindings;
        
        void Update()
        {
            foreach (var binding in bindings)
                binding.Update();
        }
        
        public void RequestRemap(SO actionId, KeyCode[] keys)
        {
            foreach (var binding in bindings)
                binding.RequestRemap(actionId, keys);
        }
    }

    [Serializable]
    public class Keybinding
    {
        [SerializeField] KeyCode[] keys;
        [SerializeField] PressType pressType;
        [SerializeField] UnityEvent OnActive;
        
        [Header("Experimental")]
        [SerializeField] SO actionId;
        
        public void RequestRemap(SO actionId, KeyCode[] keys)
        {
            if (this.actionId == actionId)
                this.keys = keys;
        }
        
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