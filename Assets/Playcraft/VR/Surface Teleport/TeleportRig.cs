using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.VR
{
    public class TeleportRig : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform rig;
        [SerializeField] Transform head;
        [SerializeField] UnityEvent OnTeleport;
        #pragma warning restore 0649

        bool success;
        Vector3 destination;

        public void InputDestination(Vector3 destination) { this.destination = destination; }
        public void InputResult(Trinary result) { success = result == Trinary.True; }
        
        public void RequestTeleport()
        {
            if (!success) return;
            
            var offset = rig.transform.position - head.transform.position;
            destination += new Vector3(offset.x, 0f, offset.z);
            rig.transform.position = destination;
            OnTeleport.Invoke();
        }
    }
}
