using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "Playcraft/Predictive Movement/Destination Modifier/Position History", fileName = "Position History")]
    public class ApplyPositionHistory : DestinationModifier
    {
        public PositionHistory process;
        public override Vector3 Tick(Vector3 value) { return process.Tick(value); }
    }
}