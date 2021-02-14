using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FilterCollidersByCustomTag : MonoBehaviour
    {
        [SerializeField] Binding[] bindings;
                        
        public void Input(List<Collider> values)
        {
            foreach (var binding in bindings)
                binding.Input(values);
        }
    }
    
    [Serializable] public class Binding
    {
        [SerializeField] SO[] validTags;
        [SerializeField] ColliderListEvent Response;
        
        CustomTags _tagged; 
        List<Collider> validColliders = new List<Collider>();
        
        public void Input(List<Collider> values)
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
            
            Response.Invoke(validColliders);           
        }
    }
}
