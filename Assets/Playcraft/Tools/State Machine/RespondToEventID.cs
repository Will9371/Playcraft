using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class RespondToEventID : MonoBehaviour
    {
        [SerializeField] EventResponse[] elements = default;

        public void Input(TagSO value)
        {
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
