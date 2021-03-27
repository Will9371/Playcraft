using UnityEngine;
using UnityEngine.UI;

namespace Playcraft.Examples.SwordTrainer
{
    public class SwingTargetTest : MonoBehaviour, ISwingTarget
    {
        [SerializeField] Text speedDisplay;
        [SerializeField] Text edgeDisplay;
        [SerializeField] Text scoreDisplay;
        [SerializeField] Transform directionOrb;
        
        public void SendData(SwingData data)
        {
            directionOrb.localPosition = -data.direction;
            
            speedDisplay.text = data.speed.ToString("F2");
            edgeDisplay.text = data.edgeAlignment.ToString("F2");
            
            var score = Mathf.Pow(data.speed * data.edgeAlignment * 10f, 2f);
            scoreDisplay.text = score.ToString("F2");
        }
    }
}
