using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FilterCollidersByCustomTagMono : MonoBehaviour
    {
        [SerializeField] CustomTagColliderFilter[] bindings;
                        
        public void Input(List<Collider> values)
        {
            foreach (var binding in bindings)
                binding.Input(values);
        }
        
        [Serializable] public class CustomTagColliderFilter
        {
            [SerializeField] ColliderListEvent Response;
            FilterCollidersByCustomTag process;
            public void Input(List<Collider> values) { Response.Invoke(process.Input(values)); }
        }
    }
}
