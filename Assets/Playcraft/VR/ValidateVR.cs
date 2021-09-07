using UnityEngine;
using UnityEngine.XR.Management;

namespace Playcraft.VR
{
    public class ValidateVR : MonoBehaviour
    {
        [SerializeField] bool vrEnabled;
        [SerializeField] BoolEvent Relay;
        
        XRGeneralSettings settings => XRGeneralSettings.Instance;
        
        bool initialized;
        bool priorEnabled;
        
        void OnValidate()
        {
            if (initialized && priorEnabled == vrEnabled || !settings)
                return;

            settings.InitManagerOnStart = vrEnabled;
            Relay.Invoke(vrEnabled);
            
            initialized = true;
            priorEnabled = vrEnabled;
        }
    }
}
