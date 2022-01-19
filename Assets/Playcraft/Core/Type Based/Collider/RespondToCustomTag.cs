using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class RespondToCustomTag
    {
        public SO[] validTags;
            
        public bool Input(Collider other) { return Input(other.transform); }
        public bool Input(Collider2D other) { return Input(other.transform); }
        
        public bool Input(Transform other)
        {
            if (!other.TryGetComponent<CustomTags>(out var customTags))
                return false;
            
            foreach (var validTag in validTags)
                if (customTags.HasTag(validTag))
                    return true;
                        
            return false;                 
        }
    }
}
