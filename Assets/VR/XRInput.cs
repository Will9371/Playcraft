using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable] public class BoolEvent : UnityEvent<bool> { }

public class XRInput : MonoBehaviour
{
    [SerializeField] XRController controller;         
    [SerializeField] XRBinding[] bindings;
                
    void Update()
    {               
        foreach (var binding in bindings)
            binding.Update(controller.inputDevice);
    }
}

[Serializable]
public class XRBinding
{
    [SerializeField] XRButton button;
    [SerializeField] PressType condition;
    [SerializeField] UnityEvent OnActive;

    bool isPressed;
    bool wasPressed;
    
    public void Update(InputDevice device)
    {
        device.TryGetFeatureValue(XRStatics.GetFeature(button), out isPressed);
        
        switch (condition)
        {
            case PressType.Continuous: if (isPressed) OnActive.Invoke(); break;
            case PressType.Begin: if (isPressed && !wasPressed) OnActive.Invoke(); break;
            case PressType.End: if (!isPressed && wasPressed) OnActive.Invoke(); break;
        }
        
        wasPressed = isPressed;
    }
}

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
            case XRButton.Menu: return CommonUsages.menuButton; 
            default: Debug.LogError(button + " not found"); return CommonUsages.primaryButton;
        }
    }
}

public enum PressType
{
    Begin, End, Continuous
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
    Primary2DAxisTouch,
    Menu
}