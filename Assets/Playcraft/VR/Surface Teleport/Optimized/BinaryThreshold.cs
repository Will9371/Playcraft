using UnityEngine;

namespace Playcraft.Optimized
{
    public class BinaryThreshold
    {
        readonly Vector2 thresholds;
        
        public BinaryThreshold(Vector2 thresholds)
        {
            this.thresholds = thresholds;
        }
            
        bool isHigh;

        public bool Input(Vector2 value) { return Input(value.magnitude); }
        public bool Input(float value)
        {
            if (!isHigh && value > thresholds.y)
                isHigh = true;
            else if (isHigh && value < thresholds.x)
                isHigh = false;
                
            return isHigh;
        }
    }
}
