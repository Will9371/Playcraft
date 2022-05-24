using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Scenes/Grid Layout")]
    public class GridInfo : ScriptableObject
    {
        public Object start;
        public GridCell[] bindings;
        
        public GridCell GetBinding(Object value)
        {
            foreach (var binding in bindings)
                if (binding.value == value)
                    return binding;
                    
            return null;
        }
    }

    [Serializable]
    public class GridCell
    {
        public Object value;
        public List<Object> adjacent;
    }
}