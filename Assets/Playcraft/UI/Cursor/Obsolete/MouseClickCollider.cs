using System;
using UnityEngine;
using UnityEngine.Events;

// DEPRECATED: use RespondToMouse

// Converts Mouse Events to UnityEvent pattern. Only works for left-click.
// For left/right click detection, use GetMouseInput + MouseRaycast + RespondToEventID
namespace Playcraft
{
    public class MouseClickCollider : MonoBehaviour
    {
        public enum MouseEvent { Enter, Exit, Down, Up, Over, Drag, UpAsButton }

        [SerializeField] MouseEventBindings[] bindings = default;   

        void OnMouseEnter() { Respond(MouseEvent.Enter); }
        void OnMouseExit() { Respond(MouseEvent.Exit); }
        void OnMouseDown() { Respond(MouseEvent.Down); }
        void OnMouseUp() { Respond(MouseEvent.Up); }
        void OnMouseOver() { Respond(MouseEvent.Over); }
        void OnMouseDrag() { Respond(MouseEvent.Drag); }
        private void OnMouseUpAsButton() { Respond(MouseEvent.UpAsButton); }

        void Respond(MouseEvent interaction)
        {
            foreach (var binding in bindings)
                if (binding.interaction == interaction)
                    binding.Response.Invoke();
        }
        
        [Serializable] public struct MouseEventBindings
        {
            public MouseEvent interaction;
            public UnityEvent Response;
        }
    }
}
