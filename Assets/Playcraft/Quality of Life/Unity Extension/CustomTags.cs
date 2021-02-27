using UnityEngine;

namespace Playcraft
{
    public class CustomTags : MonoBehaviour
    {
        [SerializeField] SO[] tags;
        
        public bool HasTag(SO[] values)
        {
            foreach (var value in values)
                if (HasTag(value))
                    return true;
                    
            return false;
        }
        
        public bool HasTag(SO value)
        {
            foreach (var item in tags)
                if (item == value)
                    return true;
                    
            return false;
        }
    }
}
