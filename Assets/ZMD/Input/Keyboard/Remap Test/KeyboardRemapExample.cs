using UnityEngine;

namespace ZMD.Examples.KeyboardRemapping
{
    public class KeyboardRemapExample : MonoBehaviour
    {
        [SerializeField] KeyboardInputActions input;
        [SerializeField] DisplayKeybindings display;
        [SerializeField] SO activeBinding;

        void Update()
        {
            var keys = GetKey.GetCurrentKeysDown();
            if (keys == null) return;
            
            foreach (var key in keys)
                input.SetKey(activeBinding, key);
                
            display.Refresh();
        }
        
        public void SetActiveBinding(SO value) { activeBinding = value; }
    }
}
