using System;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class CheckCustomTag
    {
        public SO tag;
        
        public bool HasTag(Collider other)
        {
            var tags = other.GetComponent<CustomTags>();
            return tags != null && tags.HasTag(tag);
        }
        
        public bool HasTag(Transform other)
        {
            var tags = other.GetComponent<CustomTags>();
            return tags != null && tags.HasTag(tag);
        }
        
        public bool HasTag(GameObject other)
        {
            var tags = other.GetComponent<CustomTags>();
            return tags != null && tags.HasTag(tag);
        }
    }
}
