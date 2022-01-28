using UnityEngine;

namespace ZMD
{
    public static class RangeMath
    {
        public static float ApplyMinMax(float value, Vector2 range)
        {
            if (value < range.x) return range.x;
            if (value > range.y) return range.y;
            return value;
        }
        
        public static int CycleInt(int value, int max, bool clockwise = true)
        {
            value = clockwise ? value + 1 : value - 1;
            
            if (value > max)
                return 0;
            if (value < 0)
                return max;
            
            return value;
        }
        
        public static float KeepWithin(float value, float max)
        {
            if (value > max/2f) return value - max;
            if (value < -max/2f) return value + max;
            return value;
        }
        
        public static bool InRange(float value, Vector2 range) 
        { 
            return range.x <= range.y ?
                value >= range.x && value <= range.y :
                value >= range.y && value <= range.x;
        }
    }
}
