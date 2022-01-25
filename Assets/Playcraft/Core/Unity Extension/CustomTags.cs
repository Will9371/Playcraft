using UnityEngine;

namespace ZMD
{
    public class CustomTags : MonoBehaviour
    {
        public SO[] tags;
        
        public bool HasTag(SO value)
        {
            foreach (var item in tags)
                if (item == value)
                    return true;
                    
            return false;
        }
        
        public bool HasAnyTag(SO[] values)
        {
            foreach (var value in values)
                if (HasTag(value))
                    return true;
                    
            return false;
        }
    }
}
