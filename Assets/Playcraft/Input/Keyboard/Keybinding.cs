using System;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class Keybinding
    {
        [Tooltip("Not needed for simple KeyboardInput component.  " +
                 "Needed to identify actions in remappable KeyboardInputAction system")]
        public SO id;
        public KeyCode[] keys;
        public PressType pressType;

        public bool IsActive(SO id = null)
        {
            if (id && id != this.id)
                return false;
        
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
                
            return active;            
        }
        
        /// Remap keys for a keybinding, limited to a single value
        public void SetKeys(KeyCode value) { keys = new[]{value}; }
        
        /// Remap keys for a keybinding
        public void SetKeys(KeyCode[] values) { keys = values; }
        
        /// Remap one key from an array of keys in a keybinding
        public void SetKey(KeyCode value, int index)
        {
            if (index >= keys.Length) return;
            keys[index] = value;
        }
    }  
}