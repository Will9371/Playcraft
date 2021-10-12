using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class Keybinding
    {
        [SerializeField] KeyCode[] keys;
        [SerializeField] PressType pressType;

        public bool IsActive()
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
                
            return active;            
        }
    }  
}
