using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZMD.Examples.KeyboardRemapping
{
    public class DisplayKeybindings : MonoBehaviour
    {
        [SerializeField] Keybindings keybindings;
        [SerializeField] Display[] fields;

        void Start() { Refresh(); }
        
        public void Refresh()
        {
            foreach (var field in fields)
            {
                var binding = keybindings.GetBinding(field.id);
                if (binding == null) continue;
                
                field.label.text = binding.id.name + ": ";
                field.key.text = binding.keys[0].ToString();
            }
        }

        [Serializable]
        public struct Display
        {
            public SO id;
            public Text label;
            public Text key;
        }
    }
}


