using UnityEngine;

namespace Playcraft
{
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
