using UnityEngine;

namespace Playcraft
{
    public class CustomTags : MonoBehaviour
    {
        [SerializeField] SO[] tags;
        
        public bool HasTag(SO value)
        {
            foreach (var item in tags)
                if (item == value)
                    return true;
                    
            return false;
        }
    }
}
