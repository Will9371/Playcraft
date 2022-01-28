using System;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class GetChildFromKeyboardInput
    {
        public Transform parent;
        public NumericKeyboardInput input;
        
        int childCount => parent.childCount;

        public Transform GetFirstOrNull()
        {
            if (!parent) return null;
        
            input.Update();
            foreach (var activeValue in input.activeValues)
                if (activeValue < childCount)
                    return parent.GetChild(activeValue);
                    
            return null;
        }
    }
}
