#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.XR.Management;

namespace Playcraft.VR
{
    public class ValidateVR : MonoBehaviour
    {
        [SerializeField] bool vrEnabled;
        [SerializeField] bool enableObjectActivation = true;
        [SerializeField] GameObject[] activeInFlatscreen;
        [SerializeField] GameObject[] activeInVr;
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
            
            if (!enableObjectActivation)
                return;
            if (activeInFlatscreen == null || activeInVr == null)
                return;
            
            foreach (var item in activeInFlatscreen)
                item.SetActive(!vrEnabled);
            foreach (var item in activeInVr)
                item.SetActive(vrEnabled);
        }
    }
}

#endif