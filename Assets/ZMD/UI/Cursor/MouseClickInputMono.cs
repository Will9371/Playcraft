using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class MouseClickInputMono : MonoBehaviour
    {
        public MouseClickInput process;
        public UnityEvent Output;

        void Update()
        {
            if (process.Update())
                Output.Invoke();
        }
    }
    
    [Serializable] 
    public class MouseClickInput
    {
        public MouseButton button;
        public PressType pressType;
                                
        public bool Update()
        {
            var id = GetIntFromButton(button);
            bool active = false;
                        
            switch (pressType)
            {
                case PressType.Down: active = Input.GetMouseButtonDown(id); break;
                case PressType.Up: active = Input.GetMouseButtonUp(id); break;
                case PressType.Continuous: active = Input.GetMouseButton(id); break;
            }
                        
            return active;
        }
                
        int GetIntFromButton(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left: return 0;
                case MouseButton.Right: return 1;
                case MouseButton.Center: return 2;
            }
                
            return -1;
        }
    }
}
