using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class ClickDetectMono : MonoBehaviour
    {
        public ClickDetect process;
        public UnityEvent OnClick;
        void Update() { if (process.Update()) OnClick.Invoke(); }
        void OnValidate() { process.OnValidate(); }
    }

    [Serializable]
    public class ClickDetect
    {
        public MouseButton button;
        public float maxClickTime = 0.2f; 
        
        [HideInInspector] public MouseClickInput[] click = new MouseClickInput[2];
        
        const int down = 0;
        const int up = 1;
        
        public void OnValidate()
        {
            click[down].button = button;
            click[down].pressType = PressType.Down;
            click[up].button = button;
            click[up].pressType = PressType.Up;
        }
        
        float startTime;
    
        public bool Update()
        {
            var hasPressedDown = click[down].Update();
            var hasPressedUp = click[up].Update();
            
            if (hasPressedDown) startTime = Time.time;
            return hasPressedUp && startTime != 0f && Time.time - startTime < maxClickTime;
        }
    }
}
