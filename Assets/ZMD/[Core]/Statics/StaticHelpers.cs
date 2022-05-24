using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace ZMD
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

        public static Transform[] GetChildren(Transform parent)
        {
            if (!parent) return null;
            
            var result = new Transform[parent.childCount];
            
            for (int i = 0; i < parent.childCount; i++)
                result[i] = parent.GetChild(i);
                
            return result;
        }
        
        public static Vector3[] GetPositions(Transform[] locations, bool useLocal)
        {
            if (locations == null) return null;
        
            var result = new Vector3[locations.Length];
            
            for (int i = 0; i < locations.Length; i++)
                result[i] = useLocal ? locations[i].localPosition : locations[i].position;
                
            return result;
        }
        
        // * Generalize with Generics
        public static Transform[] RemoveNullFromArray(Transform[] array)
        {
            var list = array.ToList();
            
            for (int i = list.Count - 1; i >= 0; i--)
                if (!list[i])
                    list.Remove(list[i]);
                    
            return list.ToArray();
        }
    }
}
