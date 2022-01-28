using System;
using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "Playcraft/Input/Directional Keybindings")]
    public class DirectionalKeybindings : ScriptableObject
    {
        public Binding[] values;
        
        [Serializable]
        public class Binding
        {
            public Keybinding input;
            public Vector3 value;
        }
    }
}
