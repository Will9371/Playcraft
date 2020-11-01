using UnityEngine;

namespace Playcraft.FPS
{
    [CreateAssetMenu(menuName = "Playcraft/FPS/Laser")]
    public class LaserData : ScriptableObject
    {
        public float speed;
        public float range;
    }
}
