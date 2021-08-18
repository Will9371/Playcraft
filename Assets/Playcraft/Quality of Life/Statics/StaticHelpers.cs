using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public static class StaticHelpers
    {
        #region Check Collider for Custom Tag
        
        public static bool CollisionHasCustomTag(Collision other, SO[] validTags)
        {
            return ColliderHasCustomTag(other.collider, validTags);
        }
    
        public static bool ColliderHasCustomTag(Collider other, SO[] validTags)
        {
            var otherTag = other.GetComponent<CustomTags>();
            return otherTag && otherTag.HasAnyTag(validTags);
        }
        
        public static bool CollisionHasCustomTag(Collision other, SO validTag)
        {
            return ColliderHasCustomTag(other.collider, validTag);
        }
    
        public static bool ColliderHasCustomTag(Collider other, SO validTag)
        {
            var otherTag = other.GetComponent<CustomTags>();
            return otherTag && otherTag.HasTag(validTag);
        }
        
        public static bool CustomTagNearPosition(Vector3 center, float radius, SO tag)
        {
            var overlap = Physics.OverlapSphere(center, radius);
            
            foreach (var found in overlap)
                if (ColliderHasCustomTag(found, tag))
                    return true;

            return false;
        }
        
        public static bool CustomTagNearPosition(Vector3 center, float radius, SO[] tags)
        {
            var overlap = Physics.OverlapSphere(center, radius);
            
            foreach (var found in overlap)
                if (ColliderHasCustomTag(found, tags))
                    return true;

            return false;
        }
        
        public static List<SO> GetCustomTagsNearPosition(Vector3 center, float radius)
        {
            var result = new List<SO>();
            var overlap = Physics.OverlapSphere(center, radius);
            
            foreach (var found in overlap)
            {
                var tags = found.GetComponent<CustomTags>();
                if (tags != null) result.AddRange(tags.tags);
            }
            
            return result;
        }
        
        public static void DisableObjectsNearPosition(Vector3 center, float radius)
        {
            var overlap = Physics.OverlapSphere(center, radius);
            foreach (var found in overlap)
                found.gameObject.SetActive(false);
        }
        
        #endregion
    }
}
