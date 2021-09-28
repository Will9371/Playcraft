using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{ 
    [CreateAssetMenu(menuName = "Playcraft/VR/Sword Trainer/Parry Target Settings")]
    public class ParryTargetSettings : ScriptableObject
    {
        public Vector3Array positionData;
        public Vector3Array rotationData;
        
        public float holdTime = 1.5f;
        public float transitionTime = 0.3f;
    }
}
