using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Character/Turn Direction")]
    public class TurnDirection : ScriptableObject
    {
        public Axis axis;
        public bool clockwise;
    }
}
