using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    [CreateAssetMenu(menuName = "Playcraft/VR/Sword Trainer/Cut Target Settings")]
    public class CutTargetSettings : ScriptableObject
    {
        public float rotationSpeed;
        public float timeToExtend;
        public float timeToRetract;
        public float retractDelay = 0.3f;
        public float delayRotationTime;
        public float targetScale = .4f;
        public Vector3[] targetSpread;
        public FloatArray angles;
        public bool barriersActive;
        public Vector3 barrierScale;
        public Vector3[] barrierPositions;
        
        public Vector3 scale => new Vector3(targetScale, targetScale, targetScale);
    }
}
