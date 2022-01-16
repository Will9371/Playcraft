using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Experimental/Average Velocity", fileName = "Average Velocity Settings")]
    public class AverageVelocitySO : ScriptableObject
    {
        public int runningAverageLength = 10;
        [Tooltip("1f = no effect, 0.5f = 50% cutoff per tick, 0f = full attenuation in one frame")]
        [Range(0, 1)] public float attenuationFactor = 1f;
        public float projectionTime = .5f;
    }
}