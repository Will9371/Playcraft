using System.Collections.Generic;
using UnityEngine;

namespace ZMD
{
    public class GetKeyMono : MonoBehaviour
    {
        [SerializeField] KeyCodeEvent keyDown;
        [SerializeField] KeyCodeEvent keyHold;
        [SerializeField] KeyCodeEvent keyUp;

        bool useKeyDown;
        bool useKeyHold;
        bool useKeyUp;
        
        void OnValidate()
        {
            useKeyDown = keyDown.GetPersistentEventCount() > 0;
            useKeyHold = keyHold.GetPersistentEventCount() > 0;
            useKeyUp = keyUp.GetPersistentEventCount() > 0;
        }
        
        IEnumerable<KeyCode> keys;

        void Update()
        {
            if (useKeyDown) Output(GetKey.GetCurrentKeysDown(), keyDown);
            if (useKeyHold) Output(GetKey.GetCurrentKeys(), keyHold);
            if (useKeyUp) Output(GetKey.GetCurrentKeysUp(), keyUp);
        }
        
        void Output(IEnumerable<KeyCode> keys, KeyCodeEvent output)
        {
            if (keys == null) return; 
            foreach (var key in keys)
                output.Invoke(key);
        }
    }
}