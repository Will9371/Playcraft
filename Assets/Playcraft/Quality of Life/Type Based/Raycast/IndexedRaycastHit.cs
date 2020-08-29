using UnityEngine;

namespace Playcraft
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
