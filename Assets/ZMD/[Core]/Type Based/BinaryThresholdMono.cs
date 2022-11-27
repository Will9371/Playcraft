using UnityEngine;
using UnityEngine.Events;

// Example usage: VR Arc teleport
namespace ZMD
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
    
    public class BinaryThreshold
    {
        readonly Vector2 thresholds;
        bool isHigh;
        
        public BinaryThreshold(Vector2 thresholds, bool startHigh)
        {
            this.thresholds = thresholds;
            isHigh = startHigh;
            wasHigh = startHigh;
        }
            
        public bool Input(Vector2 value) { return Input(value.magnitude); }
        public bool Input(float value)
        {
            if (!isHigh && value > thresholds.y) isHigh = true;
            else if (isHigh && value < thresholds.x) isHigh = false;
            return isHigh;
        }
        
        bool wasHigh;
        
        public Trinary DetectChange(float value)
        {
            Input(value);
    
            var result = Trinary.Unknown;
            if (!wasHigh && isHigh) result = Trinary.True;
            if (wasHigh && !isHigh) result = Trinary.False;
            
            wasHigh = isHigh;
            return result;
        }
    }
}
