using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class RespondToEventID : MonoBehaviour
    {
        [SerializeField] EventResponse[] elements = default;
        
        [SerializeField] bool locked = false;
        public void SetLock(bool value) { locked = value; }

        public void Input(TagSO value)
        {
            if (locked) return;
                
            foreach (var item in elements)
                if (item.id == value)
                    item.Response.Invoke();
        }
        
        [Serializable] struct EventResponse
        {
            #pragma warning disable 0649
            public TagSO id;
            public UnityEvent Response;
            #pragma warning restore 0649
        }
    }
}
