using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    [CreateAssetMenu(menuName = "Playcraft/VR/Sword Trainer/Cut Quality")]
    public class CutQualityCalculator : ScriptableObject
    {
        [SerializeField] float power = 2f;
        [SerializeField] float multiplier = 10f;
    
        public float GetCutQuality(SwingData data)
        {
            return Mathf.Pow(data.speed * data.edgeAlignment * multiplier, power);
        }
    }
}

