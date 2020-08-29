using UnityEngine;

namespace Playcraft.VR
{
    public struct TeleportResult
    {
        public Vector3 cutoffPoint;
        public Trinary success;
        
        public TeleportResult(Vector3 cutoffPoint, Trinary success)
        {
            this.cutoffPoint = cutoffPoint;
            this.success = success;
        }
    }
}
