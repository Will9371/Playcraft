using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class RespondToEventID : MonoBehaviour
    {
        [SerializeField] EventResponse[] elements;

        public void Input(TagSO value)
        {        
            foreach (var item in elements)
                if (item.id == value)
                    item.Response.Invoke();
        }
        
        [Serializable] struct EventResponse
        {
            public TagSO id;
            public UnityEvent Response;
        }
    }
}
