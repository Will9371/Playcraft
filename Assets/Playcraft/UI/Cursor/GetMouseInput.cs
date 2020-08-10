using System;
using UnityEngine;
using UnityEngine.Events;

// Input: Game Engine
// Process: continuous check for mouse button clicks
// Output: event triggers per type
// For interactions with specific objects (UI or collider), use RespondToMouse
namespace Playcraft
{
    public class GetMouseInput : MonoBehaviour
    {
        [SerializeField] MouseClickInput[] clickInput = default;

        private void Update()
        {            
            foreach (var input in clickInput)
                input.Update();                
        }
        
        [Serializable] class MouseClickInput
        {
            #pragma warning disable 0649
            [SerializeField] MouseButton button;
            [SerializeField] PressType pressType;
            [SerializeField] UnityEvent Output;
            #pragma warning restore 0649
                            
            public void Update()
            {
                var id = GetIntFromButton(button);
                bool active = false;
                    
                switch (pressType)
                {
                    case PressType.Down: active = Input.GetMouseButtonDown(id); break;
                    case PressType.Up: active = Input.GetMouseButtonUp(id); break;
                    case PressType.Continuous: active = Input.GetMouseButton(id); break;
                }
                    
                if (active) Output.Invoke();
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
}
