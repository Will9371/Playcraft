using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class KeyboardInput : MonoBehaviour
    {    
        [SerializeField] Binding[] keyBindings;

        void Update()
        {
            foreach (var binding in keyBindings)
                binding.Update();
        }
        
        [Serializable]
        public class Binding
        {
            [SerializeField] Keybinding input;
            [SerializeField] UnityEvent response;
            
            public void Update()
            {
                if (input.IsActive())
                    response.Invoke();
            }
        }
    }
}