using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class SplitTrinary : MonoBehaviour
    {
        [SerializeField] TrinaryEventData[] ioPaths = default;    
        
        public void Input(Trinary value)
        {
            foreach (var branch in ioPaths)
                if (branch.value == value)
                    branch.OnActivate.Invoke();
        }
        
        [Serializable]
        public struct TrinaryEventData
        {
            public Trinary value;
            public UnityEvent OnActivate;
        }
    }
}