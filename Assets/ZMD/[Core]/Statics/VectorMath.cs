using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZMD
{
    public static class VectorMath
    {
        #region Conversions
    
        public static Vector3 Copy(Vector3 original) { return new Vector3(original.x, original.y, original.z); }
    
        public static Vector3 EqualVector3(float size) { return new Vector3(size, size, size); }
        
        public static Vector3 MousePosition() { return Vector2to3(Input.mousePosition, Axis.Z); }
        
        public static Vector3 Vector2to3(Vector2 value, Axis axis = Axis.Y) 
        {
            switch (axis)
            { 
                case Axis.X: return new Vector3(0f, value.x, value.y);
                case Axis.Y: return new Vector3(value.x, 0f, value.y);
                case Axis.Z: return new Vector3(value.x, value.y, 0f);
                default: return new Vector3(value.x, 0f, value.y);
            }
        }
        
        public static Vector2 Vector3to2(Vector3 value) { return new Vector2(value.x, value.z); }
        
        public static Vector2 DegreeToVector2(float degree, Vector2 zeroVector)
        {
            var offset = Vector2ToDegree(zeroVector);
            return DegreeToVector2(degree + offset);
        }
        
        public static Vector2 DegreeToVector2(float degree) { return RadianToVector2(degree * Mathf.Deg2Rad); }
        
        // Not tested
        public static Vector2 RadianToVector2(float radian, Vector2 zero)
        {
            var offset = Vector2ToDegree(zero) * Mathf.Deg2Rad;
            return RadianToVector2(radian + offset);
        }
        
        // Assumes 0 = up
        public static Vector2 RadianToVector2(float radian) { return new Vector2(Mathf.Sin(radian), Mathf.Cos(radian)); }
        
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
        
        public static float Vector2ToDegree(Vector2 vector) { return Vector2ToDegree(vector, Vector2.up); }
        
        public static float Vector2ToDegree(Vector2 value, Vector2 zero)
        {
            float angle = Vector2.Angle(zero, value);
            Vector3 cross = Vector3.Cross(zero, value);
            if (cross.z > 0) angle = 360 - angle;
            return angle;
        }
        
        public static float AngleToDot(float angle) { return Mathf.Cos(Mathf.Deg2Rad * angle/2); }
        
        public static float DotToAngle(float dot) { return Mathf.Acos(dot) * Mathf.Rad2Deg * 2f; }
        
        public static Vector3[] TransformsToPositions(Transform[] transforms)
        {
            var positions = new Vector3[transforms.Length];
            
            for (int i = 0; i < transforms.Length; i++)
                positions[i] = transforms[i].position;

            return positions;
        }

        #endregion
    
        #region Get closest element
        
        public static Transform GetClosest(Transform[] array, Transform reference)
        {
            return GetClosest(array, reference.position);
        }
        
        public static Transform GetClosest(Transform[] array, Vector3 position)
        {
            return array[GetClosestIndex(array, position)];
        }

        public static Transform GetClosest(List<Transform> list, Vector3 position)
        {
            return list[GetClosestIndex(list, position)];
        }
        
        public static Collider GetClosest(List<Collider> list, Vector3 position)
        {
            return list[GetClosestIndex(list, position)];
        }

        public static int GetClosestIndex(Transform[] array, Transform reference)
        {
            return GetClosestIndex(array, reference.position);
        }
        
        public static int GetClosestIndex(Transform[] array, Vector3 position)
        {
            var pointList = new Vector3[array.Length];
            for (int i = 0; i < array.Length; i++)
                pointList[i] = array[i].position;
                
            return GetClosestIndex(pointList, position);
        }
        
        public static int GetClosestIndex(List<Transform> list, Vector3 position)
        {
            var pointList = new Vector3[list.Count];
            for (int i = 0; i < list.Count; i++)
                pointList[i] = list[i].position;
                
            return GetClosestIndex(pointList, position);
        }
        
        public static int GetClosestIndex(List<Collider> list, Vector3 position)
        {
            var pointList = new Vector3[list.Count];
            for (int i = 0; i < list.Count; i++)
                pointList[i] = list[i].transform.position;
                
            return GetClosestIndex(pointList, position);            
        }

        public static Vector3 GetClosest(Vector3[] array, Vector3 position)
        {
            return array[GetClosestIndex(array, position)];
        }
        
        public static int GetClosestIndex(Vector3[] array, Vector3 position, bool debug = false)
        {
            var shortestDistance = Mathf.Infinity;
            var closest = 0;
            
            for (int i = 0; i < array.Length; i++)
            {
                var distance = Vector3.Distance(array[i], position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closest = i;
                }
            }
            
            if (debug)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    var color = i == closest ? Color.red : Color.yellow;
                    Debug.DrawLine(position, array[i], color);
                }
            }
            
            return closest;             
        }
        
        public static int GetClosestIndex(Transform[] array, Vector3 reference, int priorIndex, float threshold)
        { 
            return GetClosestIndex(TransformsToPositions(array), reference, priorIndex, threshold); 
        }
        
        public static int GetClosestIndex(Vector3[] array, Vector3 reference, int priorIndex, float threshold)
        {
            var closestIndex = GetClosestIndex(array, reference);
            if (closestIndex == priorIndex || priorIndex == -1) return closestIndex;
            
            var closestDistance = Vector3.Distance(array[closestIndex], reference);
            var priorDistance = Vector3.Distance(array[priorIndex], reference);
            
            var withinThreshold = priorDistance - closestDistance < threshold;
            return withinThreshold ? priorIndex : closestIndex;
        }
        
        // DEPRECATE: Merge with GetClosest
        public static Vector3 GetClosestPoint(List<Vector3> points, Vector3 desired, Vector3 fallback)
        {
            Vector3 closestPoint = fallback;
            float shortestDistance = Mathf.Infinity;
        
            foreach (var point in points)
            {
                var distance = Vector3.Distance(point, desired);
            
                if (distance < shortestDistance)
                {
                    closestPoint = point;
                    shortestDistance = distance;
                }
            }
        
            return closestPoint;
        }
        
        public static int GetClosestAngleIndex(Vector3[] angles, Vector3 desiredDirection)
        {
            var desiredAngle = Quaternion.LookRotation(desiredDirection);
            var smallestDelta = 180f;
            int result = -1;
            
            for (int i = 0; i < angles.Length; i++)
            {
                var angle = Quaternion.Euler(angles[i]);
                var delta = Quaternion.Angle(angle, desiredAngle);
                
                if (delta > smallestDelta) continue;
                
                smallestDelta = delta;
                result = i;
            }
            
            return result;
        }
        

        
        #endregion
        
        #region Sort
        
        public static Vector3[] SortPositionsByDistance(Vector3[] input, Vector3 reference)
        { 
            return input.OrderBy(x => Vector3.Distance(x, reference)).ToArray();
        } 
        
        public static Vector3[] SortPositionsByDistance(Transform[] input, Vector3 reference)
        {
            var positions = StaticHelpers.GetPositions(input, false); 
            return positions.OrderBy(x => Vector3.Distance(x, reference)).ToArray();
        }      
        
        #endregion

        #region Move Towards
        
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
        
        #endregion
        
        #region Validation

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
        
        #endregion
        
        public static void SetZRotation(Transform transform, float z)
        {
            var angle = Copy(transform.localEulerAngles);
            angle.z = z;
            transform.localEulerAngles = angle;
        }

        public static Vector3 RoundedVector(Vector3 value, float multiplier = 1f)
        {
            // Prevent divide-by-zero errors
            if (multiplier == 0f)
                return value;
                
            // Simple form for efficiency
            if (multiplier == 1f)
                return new Vector3(Mathf.Round(value.x), Mathf.Round(value.y), Mathf.Round(value.z));
        
            // General form
            return new Vector3(Mathf.Round(value.x * multiplier) / multiplier, 
                               Mathf.Round(value.y * multiplier) / multiplier, 
                               Mathf.Round(value.z * multiplier) / multiplier);
        }
        
        public static float InverseLerp(Vector3 start, Vector3 end, Vector3 value)
        {
            var startToEnd = end - start;
            var startToValue = value - start;
            var result = Vector3.Dot(startToValue, startToEnd) / Mathf.Pow(startToEnd.magnitude, 2f);
            return Mathf.Clamp01(result);
        }
        
        public static Vector3 GetAxisDirection(Transform transform, Axis axis)
        {
            switch (axis)
            {
                case Axis.X: return transform.right;
                case Axis.Y: return transform.up;
                case Axis.Z: return transform.forward;
                default: return transform.forward;
            }
        }
        
        #region Consider moving elsewhere
        
        public static float PercentOnBox(BoxCollider box, float value, Axis axis)
        {
            var boxPosition = VectorDimension(box.transform.position, axis);
            var boxCenter = VectorDimension(box.center, axis);
            var boxScale = VectorDimension(box.size, axis);
            
            var ownCenter = boxPosition + boxCenter;
            var ownHalfScale = boxScale/2f;
            var ownMinHeight = ownCenter - ownHalfScale;
            var ownMaxHeight = ownCenter + ownHalfScale;
            
            return Mathf.InverseLerp(ownMinHeight, ownMaxHeight, value);
        }
        
        public static float VectorDimension(Vector3 vector, Axis axis)
        {
            switch (axis)
            {
                case Axis.X: return vector.x;
                case Axis.Y: return vector.y;
                case Axis.Z: return vector.z;
                default: Debug.LogError($"Invalid axis {axis}"); return 0f;
            }
        }
        
        #endregion
    }
}
