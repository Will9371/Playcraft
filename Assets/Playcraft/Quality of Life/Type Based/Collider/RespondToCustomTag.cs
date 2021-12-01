using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class RespondToCustomTag
    {
        public SO[] validTags;
            
        public bool Input(Collider other)
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
