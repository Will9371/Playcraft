using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "Playcraft/Character/Turn Direction")]
    public class TurnDirection : ScriptableObject
    {
        public Axis axis;
        public bool clockwise;
    }
}
