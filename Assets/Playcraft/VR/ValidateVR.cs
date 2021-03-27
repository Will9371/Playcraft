using UnityEngine;
using UnityEngine.XR.Management;

namespace Playcraft.VR
{
    public class ValidateVR : MonoBehaviour
    {
        [SerializeField] bool vrEnabled;
        [SerializeField] SetObjectsActive activateObjects;
        
        XRGeneralSettings settings => XRGeneralSettings.Instance;
        
        bool initialized;
        bool priorEnabled;
        
        void OnValidate()
        {
            if (initialized && priorEnabled == vrEnabled)
                return;

            settings.InitManagerOnStart = vrEnabled;
            activateObjects.Input(vrEnabled);
            
            initialized = true;
            priorEnabled = vrEnabled;
        }
    }
}
