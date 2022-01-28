using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZMD
{
    /// Like KeyboardInput, but maps bindings to SO-based IDs (necessary for remappable input)
    [Serializable]
    public class KeyboardInputActions
    {
        public Keybindings bindings;
        
        [HideInInspector]
        public List<SO> activeValues = new List<SO>();
        
        public List<SO> Update()
        {
            activeValues.Clear();
            if (!bindings) return activeValues;
            
            foreach (var binding in bindings.values)
                if (binding.IsActive())
                    activeValues.Add(binding.id);

            return activeValues;
        }
        
        /// Remap keys for a keybinding, limited to a single value
        public void SetKey(SO id, KeyCode value) 
        { 
            foreach (var binding in bindings.values)
                if (id == binding.id)
                    binding.SetKeys(value);
        }
        
        /// Remap keys for a keybinding
        public void SetKeys(SO id, KeyCode[] values) 
        { 
            foreach (var binding in bindings.values)
                if (id == binding.id)
                    binding.SetKeys(values);
        }
    }
}