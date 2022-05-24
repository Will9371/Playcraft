using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

// NOT TESTED
namespace ZMD.VR
{
    public class RecenterVRCamera : MonoBehaviour
    {
        public void Trigger()
        {
            var settings = XRGeneralSettings.Instance;
            var manager = settings.Manager;
            var loader = manager.activeLoader;
            var input = loader.GetLoadedSubsystem<XRInputSubsystem>();
            input.TryRecenter();
        }
    }
}