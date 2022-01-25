using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class NumericKeyboardInput
    {
        public Keybindings bindings;
        
        [HideInInspector]
        public List<int> activeValues = new List<int>();
        
        public List<int> Update()
        {
            activeValues.Clear();
        
            for (int i = 0; i < bindings.values.Length; i++)
                if (bindings.values[i].IsActive())
                    activeValues.Add(i);

            return activeValues;
        }
    }
}
