using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInput2D : MonoBehaviour
{
    [SerializeField] XRController controller;
    [SerializeField] Vector2Event OnActive;
    
    Vector2 value;
    
    public void GetAxisValue()
    {
        controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out value);        
        OnActive.Invoke(value);
    }
}
