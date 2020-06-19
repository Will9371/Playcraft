using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Character/Turn Direction")]
    public class TurnDirection : ScriptableObject
    {
        public Axis axis;
        public bool clockwise;
    }
}
