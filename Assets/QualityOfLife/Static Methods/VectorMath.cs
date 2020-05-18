using System.Collections.Generic;
using UnityEngine;

public static class VectorMath
{
    public static Transform GetClosest(List<Transform> list, Vector3 position)
    {
        var shortestDistance = Mathf.Infinity;
        var closest = list[0];
        
        for (int i = 0; i < list.Count; i++)
        {
            var distance = Vector3.Distance(list[i].position, position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = list[i];
            }
        }
        
        return closest;
    }
    
    public static Vector3 Vector2to3(Vector2 value)
    {
        return new Vector3(value.x, 0f, value.y);
    }
    
    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    
    // Assumes 0 = up
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
    }
    
    public static Vector2 Rotate(Vector2 vector, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(vector.x * cos - vector.y * sin, vector.x * sin + vector.y * cos);
    }
    
    public static Vector2 PerspectiveVector(Vector2 input, Vector2 source, Vector2 target)
    {    
        var angle = AngleDirection(input) + AngleDirection(source) - AngleDirection(target);
        return DegreeToVector2(angle);
    }
    
    public static float AngleDirection(Vector2 vector)
    {
        float angle = Vector2.Angle(Vector2.up, vector);
        Vector3 cross = Vector3.Cross(Vector2.up, vector);
        if (cross.z > 0) angle = 360 - angle;
        return angle;
    }
}
