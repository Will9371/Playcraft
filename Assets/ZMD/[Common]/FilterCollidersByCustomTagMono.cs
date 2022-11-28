using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZMD
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
    
    [Serializable]
    public class FilterCollidersByCustomTag
    {
        [SerializeField] SO[] validTags;
        
        CustomTags _tagged; 
        List<Collider> validColliders = new();
            
        public List<Collider> Input(List<Collider> values)
        {
            validColliders.Clear();

            foreach (var value in values)
            {
                _tagged = value.GetComponent<CustomTags>();
                if (!_tagged) continue;
                    
                foreach (var validTag in validTags)
                    if (_tagged.HasTag(validTag))
                        validColliders.Add(value);
            }
                
            return validColliders;             
        }
    }
}
