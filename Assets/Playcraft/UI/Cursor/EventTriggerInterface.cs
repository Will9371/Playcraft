using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Purpose: detect interactions with UI elements
// Input: pointer data from Unity's EventTrigger component
// Process: filter data by button id and interaction type
// Output: event triggers per type
namespace Playcraft
{
    public class EventTriggerInterface : MonoBehaviour
    {
        [SerializeField] PointerBinding[] bindings;

        public void PointerDown(BaseEventData data) { Respond(data, PressType.Down); }
        public void PointerUp(BaseEventData data) { Respond(data, PressType.Up); }
        
        void Respond(BaseEventData data, PressType interaction)
        {
            var button = ((PointerEventData)data).button;
        
            foreach (var binding in bindings)
                if (binding.button == button && binding.interaction == interaction)
                    binding.Response.Invoke();
        }

        [Serializable] public struct PointerBinding
        {
            public PointerEventData.InputButton button;
            public PressType interaction;
            public UnityEvent Response;
        }    
    }
}
