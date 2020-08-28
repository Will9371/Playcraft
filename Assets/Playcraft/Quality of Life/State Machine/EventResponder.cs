using System;
using UnityEngine.Events;

// Input: event ID
// Process: editor-defined lookup table
// Returns: UnityEvent response if success, null if fail
[Serializable] public class EventResponder
{
    public EventResponse[] elements;

    public UnityEvent GetResponse(TagSO value)
    {                
        foreach (var item in elements)
            if (item.id == value)
                return item.Response;
                
        return null; 
    }

    [Serializable] public struct EventResponse
    {
        public TagSO id;
        public UnityEvent Response;
    }
}
