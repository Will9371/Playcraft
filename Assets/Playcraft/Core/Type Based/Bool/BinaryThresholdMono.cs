using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class BinaryThresholdMono : MonoBehaviour
    {
        [SerializeField] Vector2 thresholds;
        [SerializeField] UnityEvent OutputHigh;
        [SerializeField] UnityEvent OutputLow;
        [SerializeField] bool startHigh;
        
        BinaryThreshold binary;
        
        void Awake()
        {
            binary = new BinaryThreshold(thresholds, startHigh);
        }
        
        public void Input(Vector2 value) { Input(value.magnitude); }
        public void Input(float value)
        {
            var change = binary.DetectChange(value);
            if (change == Trinary.True) OutputHigh.Invoke();
            else if (change == Trinary.False) OutputLow.Invoke();
        }
    }
}
