using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public static class VectorMath
    {
        public static Vector3 EqualVector3(float size)
        {
            return new Vector3(size, size, size);
        }
    
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
        
        public static Vector2 Vector3to2(Vector3 value)
        {
            return new Vector2(value.x, value.z);
        }
        
        public static Vector2 DegreeToVector2(float degree, Vector2 zeroVector)
        {
            var offset = Vector2ToDegree(zeroVector);
            return DegreeToVector2(degree + offset);
        }
        
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
        
        // Not tested
        public static Vector2 RadianToVector2(float radian, Vector2 zero)
        {
            var offset = Vector2ToDegree(zero) * Mathf.Deg2Rad;
            return RadianToVector2(radian + offset);
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
            var angle = Vector2ToDegree(input) + Vector2ToDegree(source) - Vector2ToDegree(target);
            return DegreeToVector2(angle);
        }
        
        public static float Vector2ToDegree(Vector2 vector)
        {
            return Vector2ToDegree(vector, Vector2.up);
        }
        
        public static float Vector2ToDegree(Vector2 value, Vector2 zero)
        {
            float angle = Vector2.Angle(zero, value);
            Vector3 cross = Vector3.Cross(zero, value);
            if (cross.z > 0) angle = 360 - angle;
            return angle;
        }
        
        public static float AngleToDot(float angle)
        {
            return Mathf.Cos(Mathf.Deg2Rad * angle/2);
        }
        
        public static float DotToAngle(float dot)
        {
            return Mathf.Acos(dot) * Mathf.Rad2Deg * 2f;
        }
        
        public static float MoveTowards(float current, float target, float speed)
        {
            if (current == target) return current;
            
            var direction = current < target ? 1 : -1;
            current += speed * direction * Time.deltaTime;
            
            var overshoot = direction == 1 ? current > target : current < target;
            if (overshoot) current = target;
            
            return current;
        }
        
        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float speed)
        {
            var x = MoveTowards(current.x, target.x, speed);
            var y = MoveTowards(current.y, target.y, speed);
            var z = MoveTowards(current.z, target.z, speed);
            return new Vector3(x, y, z);
        }
        
        public static Vector2 RotateTowards(Vector2 current, Vector2 target, float speed, float timeStep = 0f)
        {
            if (timeStep == 0f) timeStep = Time.deltaTime;

            var currentAngle = Vector2ToDegree(current);
            var targetAngle = Vector2ToDegree(target);

            currentAngle = RotateToAngle(currentAngle, targetAngle, speed * timeStep);

            var newDirection = DegreeToVector2(currentAngle);
            return newDirection;
        }
        
        // In degrees
        public static float RotateToAngle(float currentAngle, float desiredAngle, float speed)
        {
            // Exit early: no change requested
            if(speed == 0) return currentAngle;

            // Validate input
            desiredAngle = MakeValidAngle(desiredAngle);

            // Exit early: no change required
            if(currentAngle == desiredAngle) return currentAngle;

            // Special case: turning towards 0 degrees
            if (desiredAngle == 0f) return RotateToZero(currentAngle, speed);

            // Set direction of rotation
            bool isClockwise = Mathf.DeltaAngle(currentAngle, desiredAngle) > 0;
            if (!isClockwise) speed = -speed;

            // Prevent rotation jitter by returning destination on overshoot
            // Ignore edge case of crossing zero because it yields false positives on overshoot
            bool isCrossingZero = (isClockwise && desiredAngle < currentAngle) || (!isClockwise && desiredAngle > currentAngle);
            bool isOvershoot = (isClockwise && currentAngle + speed > desiredAngle) || (!isClockwise && currentAngle + speed < desiredAngle);
            if (isOvershoot && !isCrossingZero) return desiredAngle;

            float newAngle = MakeValidAngle(currentAngle + speed);
            return newAngle;
        }

        private static float RotateToZero(float angle, float speed)
        {
            angle = angle % 360;

            if (angle > 0 && angle <= 180)
            {
                angle -= speed;
                if (angle < 0) angle = 0;
            }
            else if (angle > 180)
            {
                angle += speed;
                if (angle > 360) angle = 0;
            }
            else if (angle < 0 && angle >= -180)
            {
                angle += speed;
                if (angle > 0) angle = 0;
            }
            else if (angle < -180)
            {
                angle -= speed;
                if (angle <= -360) angle = 0;
            }

            return angle;
        }

        public static float MakeValidAngle(float value)
        {
            value %= 360;
            if(value < 0) value += 360;
            return value;
        }

        public static float MakeValidAngle(float value, float max)
        {
            max = Mathf.Clamp(max, 0, 360);
            if(IsBetween(value, 0, max)) return value;
            
            if(max != 360) Mathf.Clamp(value, 0, max);
            else
            {
                value %= max;
                if(value < 0) value += max;
            }
            return value;
        }
        
        public static bool IsBetween<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true) where T : System.IComparable<T>
        {
            return (minInclusive ? min.CompareTo(value) <= 0 : min.CompareTo(value) < 0) && (maxInclusive ? value.CompareTo(max) <= 0 : value.CompareTo(max) < 0);
        }
    }
}
