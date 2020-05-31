using UnityEngine;

namespace Playcraft
{
    public static class RangeMath
    {
        public static float ApplyMinMax(float value, Vector2 range)
        {
            if (value < range.x) return range.x;
            if (value > range.y) return range.y;
            return value;
        }
        
        public static int CycleInt(int value, int max)
        {
            value++;
            
            if (value >= max)
                value = 0;
            
            return value;
        }
        
        public static float KeepWithin(float value, float max)
        {
            if (value > max/2f) return value - max;
            if (value < -max/2f) return value + max;
            return value;
        }
    }
}
