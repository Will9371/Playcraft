using UnityEngine;

namespace Playcraft
{
    public interface ISwingTarget
    {
        void SetHitSpeed(float value);
        void SetHitDirection(Vector3 value);
        void SetHitEdge(float value);
        void Refresh();
    }
}
