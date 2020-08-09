using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class BinaryThreshold : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector2 thresholds;
        [SerializeField] UnityEvent OutputHigh;
        [SerializeField] UnityEvent OutputLow;
        #pragma warning restore 0649
        
        public bool isHigh;

        public void Input(Vector2 value) { Input(value.magnitude); }
        public void Input(float value)
        {
            if (!isHigh && value > thresholds.y)
            {
                OutputHigh.Invoke();
                isHigh = true;
            }
            else if (isHigh && value < thresholds.x)
            {
                OutputLow.Invoke(); 
                isHigh = false;
            }
        }
    }
}
