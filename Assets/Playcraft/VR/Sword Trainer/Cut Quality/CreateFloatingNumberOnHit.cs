using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class CreateFloatingNumberOnHit : MonoBehaviour, ISwingTarget
    {
        [SerializeField] CutQualityCalculator cutQualityCalculator;
        [SerializeField] CreateFloatingNumber createNumber;
        [SerializeField] FloatEvent RelayScore;

        public void SetFloaterCanvas(Transform value) { createNumber.SetFloaterCanvas(value); }
        public void SetFloaterFaceTarget(Transform value) { createNumber.SetFloaterCanvas(value); }
        
        public void SendData(SwingData data)
        {
            var score = cutQualityCalculator.GetCutQuality(data);
            Input(data.direction, score);
        }
        
        public void Input(Vector3 direction, float score)
        {
            createNumber.Generate(direction, score);
            RelayScore.Invoke(score);            
        }
    }
}
