using System;
using UnityEngine;

namespace ZMD
{
    /// Invoke events linked by ScriptableObjects in code.
    /// Lightweight alternative to ScriptableObject + UnityEvent approach.
    /// Particularly useful for communicating between scenes or dynamically generated objects
    /// where specific objects need to interact but references cannot be assigned in editor.
    [CreateAssetMenu(menuName = "ZMD/Events/Direct Actions")]
    public class EventSO : ScriptableObject
    {
        public Action onSimple;
        public Action<float> onFloat;
        public Action<Vector2> onVector2;
        public Action<Vector3> onVector3;
        
        // Add other actions as needed
    }
}