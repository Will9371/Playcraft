using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class GetCutQuality : MonoBehaviour, ISwingTarget
    {
        [SerializeField] CutQualityCalculator cutQualityCalculator;
        [SerializeField] FloatEvent relayScore;

        public void SendData(SwingData data)
        {
            var score = cutQualityCalculator.GetCutQuality(data);
            relayScore.Invoke(score);
        }
    }
}