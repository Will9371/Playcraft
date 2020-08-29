using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class BinaryThresholdComponent : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector2 thresholds;
        [SerializeField] UnityEvent OutputHigh;
        [SerializeField] UnityEvent OutputLow;
        [SerializeField] bool startHigh;
        #pragma warning restore 0649
        
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
