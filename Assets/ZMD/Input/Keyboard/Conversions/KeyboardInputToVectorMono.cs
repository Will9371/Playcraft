using System;
using UnityEngine;

namespace ZMD
{
    public class KeyboardInputToVectorMono : MonoBehaviour
    {
        [SerializeField] KeyboardInputToVector process;
        [SerializeField] Vector3Event output;
        
        void Update() { output.Invoke(process.Update()); }
    }
    
    [Serializable]
    public class KeyboardInputToVector
    {
        [SerializeField] DirectionalKeybindings bindings;
        
        Vector3 result;
        
        public Vector3 Update()
        {
            result = Vector3.zero;
        
            foreach (var binding in bindings.values)
                if (binding.input.IsActive())
                    result += binding.value;
                    
            return result.normalized;
        }
    }
}
