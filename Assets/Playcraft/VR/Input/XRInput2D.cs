using Playcraft.VR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInput2D : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] XRController controller;
    [SerializeField] Vector2Event OnActive;
    [SerializeField] bool alwaysActive;
    #pragma warning restore 0649
    
    Vector2 value;
    
    private void Update()
    {
        if (alwaysActive)
            GetAxisValue();
    }
    
    public void GetAxisValue()
    {
        XRStatics.Get2DAxisValue(controller.inputDevice);      
        OnActive.Invoke(value);
    }
}
