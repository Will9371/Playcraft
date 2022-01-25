using UnityEngine;

namespace ZMD
{
    public class TransformLine
    {
        public static Vector3 Span2Nodes(Transform spanner, Vector3 p1, Vector3 p2)
        {
            spanner.position = Midpoint(p1, p2);

            spanner.LookAt(p2, Vector3.up);
            spanner.Rotate(0f, -90f, 0f);
            
            return spanner.rotation.eulerAngles;
        }

        public static Vector3 Midpoint(Vector3 point1, Vector3 point2)
        {
            return new Vector3(Midpoint(point1.x, point2.x), Midpoint(point1.y, point2.y), Midpoint(point1.z, point2.z));
        }

        public static float Midpoint(float point1, float point2)
        {
            return (point1 - point2) / 2 + point2;
        }

        public static float Vector2Degrees(Vector2 point)
        {
            float degrees = Vector2.Angle(Vector2.up, point);

            if (point.x < 0)
                degrees += 2 * (180 - degrees);

            return degrees;
        }
    }
}
