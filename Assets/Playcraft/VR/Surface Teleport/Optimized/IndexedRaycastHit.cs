using UnityEngine;

namespace Playcraft.Optimized
{
    public struct IndexedRaycastHit
    {
        public IndexedRaycastHit(RaycastHit hit, int index)
        {
            this.hit = hit;
            this.index = index;
        }

        public RaycastHit hit;
        public int index;
    }
}
