using System;
using Playcraft;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Attach to UI elements or colliders that respond to mouse events
// For colliders, also requires attaching a graphics raycaster component to the camera
namespace Playcraft
{
    public class RespondToMouse : MonoBehaviour, 
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] ClickBinding[] bindings = default;

        public void OnPointerDown(PointerEventData data)
        {    
            Respond(data.button, PressType.Down);
        }
        
        public void OnPointerUp(PointerEventData data)
        {    
            Respond(data.button, PressType.Up);
        }
        
        public void OnPointerEnter(PointerEventData data)
        {
            Respond(data.button, PressType.Enter);
        }
        
        public void OnPointerExit(PointerEventData data)
        {    
            Respond(data.button, PressType.Exit);
        }
        
        void Respond(PointerEventData.InputButton button, PressType interaction)
        {
            foreach (var binding in bindings)
                if (binding.interaction == interaction && binding.button == button)
                    binding.Response.Invoke();        
        }
        
        [Serializable] public struct ClickBinding
        {
            public PointerEventData.InputButton button;
            public PressType interaction;
            public UnityEvent Response;
        }
    }
}
