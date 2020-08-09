using UnityEngine;

namespace Playcraft
{
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
        
        public static float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis) 
        {
            // Project A and B onto the plane orthogonal to axis
            dirA = dirA - Vector3.Project(dirA, axis);
            dirB = dirB - Vector3.Project(dirB, axis);
           
            // Find (positive) angle between A and B
            float angle = Vector3.Angle(dirA, dirB);
           
            // Return angle multiplied with 1 or -1
            return angle * (Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
        }
        
        public static Vector3 LocalVector(Transform transform, Vector3 distance)
        {
            return transform.position + transform.TransformVector(distance);
        }
    }
    public enum Axis { X, Y, Z }
}