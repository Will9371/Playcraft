using UnityEngine;
using UnityEngine.XR.Management;

namespace Playcraft.VR
{
    public class ValidateVR : MonoBehaviour
    {
        [SerializeField] bool vrEnabled;
        [SerializeField] SetObjectsActive activateObjects;
        
        XRGeneralSettings settings => XRGeneralSettings.Instance;
        
        void OnValidate()
        {
            settings.InitManagerOnStart = vrEnabled;
            activateObjects.Input(vrEnabled);
        }
    }
}
