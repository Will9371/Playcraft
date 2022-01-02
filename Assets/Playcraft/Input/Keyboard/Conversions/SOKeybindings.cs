using System;
using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Input/SO Keybindings")]
    public class SOKeybindings : ScriptableObject
    {
        public Binding[] values;
        
        [Serializable]
        public class Binding
        {
            public Keybinding input;
            public ScriptableObject value;
        }
    }
}
