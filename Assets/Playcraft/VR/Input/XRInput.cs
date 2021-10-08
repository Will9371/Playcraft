using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Playcraft.VR
{
    public class XRInput : MonoBehaviour
    {
        [SerializeField] XRController controller;
        [SerializeField] XRBinding[] bindings;
        
        private void Update()
        {
            foreach (var binding in bindings)
                binding.Update(controller.inputDevice);
        }
    }

    [Serializable]
    public class XRBinding
    {
        [SerializeField] XRButton button;
        [SerializeField] PressType pressType;
        [SerializeField] UnityEvent OnActive;
        
        bool isPressed;
        bool wasPressed;

        public void Update(InputDevice device)
        {
            isPressed = XRStatics.IsPressed(device, button);
            bool active = false;
            
            switch (pressType)
            {
                case PressType.Continuous: active = isPressed; break;
                case PressType.Down: active = isPressed && !wasPressed; break;
                case PressType.Up: active = !isPressed && wasPressed; break;
            }
            
            if (active) OnActive.Invoke();
            wasPressed = isPressed;
        }    
    }

    public enum XRButton
    {
        Trigger,
        Grip,
        Primary,
        PrimaryTouch,
        Secondary,
        SecondaryTouch,
        Primary2DAxisClick,
        Primary2DAxisTouch
    }
}