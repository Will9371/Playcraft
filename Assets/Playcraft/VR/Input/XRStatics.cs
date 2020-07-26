using UnityEngine;
using UnityEngine.XR;

namespace Playcraft.VR
{
    public static class XRStatics
    {
        public static InputFeatureUsage<bool> GetFeature(XRButton button)
        {
            switch (button)
            {
                case XRButton.Trigger: return CommonUsages.triggerButton;
                case XRButton.Grip: return CommonUsages.gripButton;
                case XRButton.Primary: return CommonUsages.primaryButton;
                case XRButton.PrimaryTouch: return CommonUsages.primaryTouch;
                case XRButton.Secondary: return CommonUsages.secondaryButton;
                case XRButton.SecondaryTouch: return CommonUsages.secondaryTouch;
                case XRButton.Primary2DAxisClick: return CommonUsages.primary2DAxisClick;
                case XRButton.Primary2DAxisTouch: return CommonUsages.primary2DAxisTouch;
                default: Debug.LogError("button " + button + " not found"); return CommonUsages.triggerButton;      
            }
        }
        
        public static bool IsPressed(InputDevice device, XRButton button)
        {
            device.TryGetFeatureValue(GetFeature(button), out bool value);
            return value;
        }
        
        public static Vector2 Get2DAxisValue(InputDevice device)
        {
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 value);
            return value;
        }
    }
}
