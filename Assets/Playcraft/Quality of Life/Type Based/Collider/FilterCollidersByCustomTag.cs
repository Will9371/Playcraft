using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FilterCollidersByCustomTag
    {
        SO[] validTags;
            
        public FilterCollidersByCustomTag(SO[] validTags) { this.validTags = validTags; }
            
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