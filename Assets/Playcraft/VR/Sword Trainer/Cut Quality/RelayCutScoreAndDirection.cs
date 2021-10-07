using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class RelayCutScoreAndDirection : MonoBehaviour, ISwingTarget
    {
        [SerializeField] Vector3FloatEvent output;
        [SerializeField] CutQualityCalculator calculator;
        [SerializeField] Transform pivot;
        [SerializeField] Vector3 localDirection;
        public bool invulnerable;
        
        public void SetInvulnerable(bool value) { invulnerable = value; }
    
        public void SendData(SwingData data)
        {
            var direction = pivot.rotation * localDirection;
            var score = invulnerable ? 0f : calculator.GetDirectionalCutQuality(data, direction);
            output.Invoke(data.direction, score);
        }
    }
}