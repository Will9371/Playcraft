using UnityEngine;
using UnityEngine.UI;

namespace Playcraft.Examples.SwordTrainer
{
    public class SwingTarget : MonoBehaviour, ISwingTarget
    {
        [SerializeField] Text speedDisplay;
        [SerializeField] Text edgeDisplay;
        [SerializeField] Text scoreDisplay;
        [SerializeField] Transform directionOrb;
        
        float speed;
        float edge;
        float score;

        public void SetHitSpeed(float value)
        {
            speed = value;
            speedDisplay.text = value.ToString("F2");
        }

        public void SetHitDirection(Vector3 value)
        {
            directionOrb.localPosition = -value;
        }
        
        public void SetHitEdge(float value)
        {
            edge = value;
            edgeDisplay.text = value.ToString("F2");
        }
        
        public void Refresh()
        {
            score = Mathf.Pow(speed * edge * 10f, 2f);
            scoreDisplay.text = score.ToString("F2");
        }
    }
}
