using System;
using UnityEngine;

namespace Playcraft
{
    public class ComponentTags : MonoBehaviour
    {
        [SerializeField] ComponentTagData[] tags = default;
        
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

public enum TagID { Stand, Teleport, Climb }

