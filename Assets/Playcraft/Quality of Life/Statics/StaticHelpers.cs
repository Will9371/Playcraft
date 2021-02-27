using UnityEngine;

namespace Playcraft
{
    public static class StaticHelpers
    {
        #region Check Collider for Custom Tag
        
        public static bool ColliderHasCustomTag(Collision other, SO[] validTags)
        {
            return ColliderHasCustomTag(other.collider, validTags);
        }
    
        public static bool ColliderHasCustomTag(Collider other, SO[] validTags)
        {
            var otherTag = other.GetComponent<CustomTags>();
            return otherTag && otherTag.HasTag(validTags);
        }
        
        public static bool ColliderHasCustomTag(Collision other, SO validTag)
        {
            return ColliderHasCustomTag(other.collider, validTag);
        }
    
        public static bool ColliderHasCustomTag(Collider other, SO validTag)
        {
            var otherTag = other.GetComponent<CustomTags>();
            return otherTag && otherTag.HasTag(validTag);
        }
        
        #endregion
    }
}
