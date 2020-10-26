using System;
using UnityEngine;

// DEPRECATE
namespace Playcraft
{
    public class RespondToEventIDWithGameObject : MonoBehaviour
    {
        [SerializeField] EventResponse[] elements = default;

        public void Input(SO value, GameObject source)
        {
            foreach (var item in elements)
                if (item.id == value)
                    item.Response.Invoke(source);
        }
        
        [Serializable] struct EventResponse
        {
            #pragma warning disable 0649
            public SO id;
            public GameObjectEvent Response;
            #pragma warning restore 0649
        }
    }
}
