using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class RespondToInt : MonoBehaviour
    {
        [SerializeField] Binding[] bindings;

        public void Input(int value)
        {
            foreach (var binding in bindings)
                if (binding.id == value)
                    binding.Response.Invoke();
        }
        
        [Serializable] public struct Binding
        {
            public int id;
            public UnityEvent Response;
        }
    }
}
