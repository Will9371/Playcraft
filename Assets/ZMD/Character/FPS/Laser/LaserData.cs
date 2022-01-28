using UnityEngine;

namespace ZMD.FPS
{
    [CreateAssetMenu(menuName = "ZMD/FPS/Laser")]
    public class LaserData : ScriptableObject
    {
        public GameObject laser;
        public GameObject explosion;
        public float speed;
        public float range;
    }
}
