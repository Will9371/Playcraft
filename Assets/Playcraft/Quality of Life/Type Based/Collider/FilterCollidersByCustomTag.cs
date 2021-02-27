using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FilterCollidersByCustomTag : MonoBehaviour
    {
        [SerializeField] CustomTagColliderFilter[] bindings;
                        
        public void Input(List<Collider> values)
        {
            foreach (var binding in bindings)
                binding.Input(values);
        }
    }
    
    [Serializable] public class CustomTagColliderFilter
    {
        [SerializeField] SO[] validTags;
        [SerializeField] ColliderListEvent Response;
        
        Custom_Tag_Collider_Filter _process;
        Custom_Tag_Collider_Filter process
        {
            get
            {
                if (_process == null)
                    _process = new Custom_Tag_Collider_Filter(validTags);
                
                return _process;
            }
        }
                
        public void Input(List<Collider> values) { Response.Invoke(process.Input(values)); }
    }
    
    public class Custom_Tag_Collider_Filter
    {
        SO[] validTags;
        
        public Custom_Tag_Collider_Filter(SO[] validTags) { this.validTags = validTags; }
        
        CustomTags _tagged; 
        List<Collider> validColliders = new List<Collider>();
        
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
