using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Playcraft.VR
{
    public class XRInput : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] XRController controller;
        [SerializeField] XRBinding[] bindings;
        #pragma warning restore 0649
        
        private void Update()
        {
            foreach (var binding in bindings)
                binding.Update(controller.inputDevice);
        }
    }

    [Serializable]
    public class XRBinding
    {
        #pragma warning disable 0649
        [SerializeField] XRButton button;
        [SerializeField] PressType pressType;
        [SerializeField] UnityEvent OnActive;
        #pragma warning restore 0649
        
        bool isPressed;
        bool wasPressed;

        public void Update(InputDevice device)
        {
            device.TryGetFeatureValue(XRStatics.GetFeature(button), out isPressed);
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