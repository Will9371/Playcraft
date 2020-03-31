using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticAxis
{
    public static Vector3 GetAxisVector(Axis axis, bool clockwise)
    {
        var direction = clockwise ? 1 : -1;
        
        switch (axis)
        {
            case Axis.X: return new Vector3(direction, 0, 0);
            case Axis.Y: return new Vector3(0, direction, 0);
            case Axis.Z: return new Vector3(0, 0, direction);
            default: Debug.LogError("Invalid axis " + axis); return Vector3.zero;
        }       
    }
}