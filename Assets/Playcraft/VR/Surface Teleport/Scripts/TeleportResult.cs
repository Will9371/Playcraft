using UnityEngine;

namespace Playcraft.VR
{
    public struct TeleportResult
    {
        public Vector3 cutoffPoint;
        public Trinary success;
        public GameObject surface;
        
        public TeleportResult(Vector3 cutoffPoint, Trinary success, GameObject surface = null)
        {
            this.cutoffPoint = cutoffPoint;
            this.success = success;
            this.surface = surface;
        }
    }
}
