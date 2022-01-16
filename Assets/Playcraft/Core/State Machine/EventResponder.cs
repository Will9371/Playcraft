using System;
using UnityEngine.Events;

// Input: event ID
// Process: editor-defined lookup table
// Returns: UnityEvent response if success, null if fail
namespace Playcraft
{
    [Serializable] public class EventResponder
    {
        public EventResponse[] elements;

        public UnityEvent GetResponse(SO value)
        {                
            foreach (var item in elements)
                if (item.id == value)
                    return item.Response;
                    
            return null; 
        }

        [Serializable] public struct EventResponse
        {
            public SO id;
            public UnityEvent Response;
        }
    }
}
