using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public enum MouseButton { None, Left, Right, Center, Any }

    [Serializable] class MouseClickInput
    {
        #pragma warning disable 0649
        [SerializeField] MouseButton button;
        [SerializeField] PressType pressType;
        [SerializeField] UnityEvent Output;
        #pragma warning restore 0649
                            
        public void Update()
        {
            var id = MouseStatics.GetIntFromButton(button);
            bool active = false;
                    
            switch (pressType)
            {
                case PressType.Down: active = Input.GetMouseButtonDown(id); break;
                case PressType.Up: active = Input.GetMouseButtonUp(id); break;
                case PressType.Continuous: active = Input.GetMouseButton(id); break;
            }
                    
            if (active) Output.Invoke();
        }
    }    
}
