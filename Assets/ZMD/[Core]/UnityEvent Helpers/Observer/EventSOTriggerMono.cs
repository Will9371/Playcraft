using System;
using UnityEngine;

namespace ZMD
{
    public class EventSOTriggerMono : MonoBehaviour
    {
        public EventSOTrigger process;
        
        public void Trigger() { process.Trigger(); }
        public void Trigger(float value) { process.Trigger(value); }
        public void Trigger(Vector2 value) { process.Trigger(value); }
        public void Trigger(Vector3 value) { process.Trigger(value); }
        
        public void Trigger(EventSO id) { process.Trigger(id); }        
    }

    [Serializable]
    public class EventSOTrigger
    {
        public EventSO id;

        public void Trigger() { id.onSimple?.Invoke(); }
        public void Trigger(float value) { id.onFloat?.Invoke(value); }
        public void Trigger(Vector2 value) { id.onVector2?.Invoke(value); }
        public void Trigger(Vector3 value) { id.onVector3?.Invoke(value); }
        
        public void Trigger(EventSO id) { id.onSimple?.Invoke(); }
        
        // Add other types as needed        
    }
}
