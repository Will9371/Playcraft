using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Predictive Movement/Destination Modifier/Average Velocity", fileName = "Average Velocity")]
    public class ApplyAverageVelocity : DestinationModifier
    {
        public AverageVelocity process;
        public override Vector3 Tick(Vector3 value) { return process.FixedUpdate(value); }
    }
}