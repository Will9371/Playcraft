using System;
using UnityEngine;

namespace Playcraft
{
    public class ComponentTags : MonoBehaviour
    {
        public ComponentTagData[] tags = default;
        
        public bool IsValid(TagID type) { return GetValidity(type) == Trinary.True; }
        public Trinary GetValidity(TagID type)
        {
            foreach (var tag in tags)
                if (tag.id == type)
                    return tag.blocker ? Trinary.False : Trinary.True;
            
            return Trinary.Unknown;
        }    
    }
    
    [Serializable] public struct ComponentTagData
    {
        public TagID id;
        public bool blocker;
    }
}
