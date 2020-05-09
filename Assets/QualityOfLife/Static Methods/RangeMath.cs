using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMath : MonoBehaviour
{
    public static float ApplyMinMax(float value, Vector2 range)
    {
        if (value < range.x) return range.x;
        if (value > range.y) return range.y;
        return value;
    }
    
    public static int CycleInt(int value, int arraySize)
    {
        value++;
        
        if (value >= arraySize)
            value = 0;
        
        return value;
    }
}
