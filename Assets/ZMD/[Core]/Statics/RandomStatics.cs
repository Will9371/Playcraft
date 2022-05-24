using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZMD
{
    public static class RandomStatics
    {
        /// 0.5 (default) = 50% chance of True; 0 = guaranteed False; 1 = guaranteed True 
        public static bool CoinToss(float center = 0.5f) { return center > Random.Range(0f, 1f); }
        
        public static WaitForSeconds RandomWait(Vector2 range) { return new WaitForSeconds(RandomInRange(range)); }
        
        public static float RandomInRange(Vector2 range) { return Random.Range(range.x, range.y); }

    
        #region Random with exclusion
    
        public static int RandomNoRepeat(int min, int max, int prior)
        {
            if (max - min < 1)
                return prior;
            
            int result = prior;
            
            while (result == prior)
                result = Random.Range(min, max);
            
            return result;
        }
        
        public static int RandomIndexNotIncluding(int max, int exclude) { return RandomIndexNotIncluding(max, new List<int>{exclude}); }
        
        /// Returns a random number less than max (including 0) and excluding any numbers in the excluded list.
        public static int RandomIndexNotIncluding(int max, List<int> excluded)
        {
            var valid = new List<int>();
            
            for (int i = 0; i < max; i++)
                valid.Add(i);
                
            foreach (var exclusion in excluded)
                valid.Remove(exclusion);

            var index = Random.Range(0, valid.Count);
            return valid[index];
        }
        
        #endregion
        
        #region Vector3
        
        public static Vector3 RandomInRectangle(MinMaxVector3 range) { return RandomInRectangle(range.min, range.max); }
        
        public static Vector3 RandomInRectangle(Vector3 min, Vector3 max)
        {
            var x = Random.Range(min.x, max.x);
            var y = Random.Range(min.y, max.y);
            var z = Random.Range(min.z, max.z);
            return new Vector3(x, y, z);
        }

        public static Vector3 RandomInHollowCylinder(Vector2 widthRange, Vector3 heightRange, Axis axis = Axis.Y)
        {
            var height = Random.Range(heightRange.x, heightRange.y);
            var width = Random.Range(widthRange.x, widthRange.y);
            var direction = Random.insideUnitCircle.normalized;
            var widthVector = direction * width;
            
            switch (axis)
            {
                case Axis.X: return new Vector3(height, widthVector.x, widthVector.y);
                case Axis.Y: return new Vector3(widthVector.x, height, widthVector.y);
                case Axis.Z: return new Vector3(widthVector.x, widthVector.y, height);
                default: return new Vector3(widthVector.x, height, widthVector.y);
            }
        }
        
        public static Vector3 RandomCubeDirection()
        {
            var normal = Random.insideUnitSphere.normalized;
            return VectorMath.RoundedVector(normal);
        }
        
        public static Vector3 RandomRotation() { return new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)); }
        
        public static Vector3 RandomInScaledSphere(Vector3 range)
        {
            var direction = Random.insideUnitSphere;
            var x = range.x * direction.x;
            var y = range.y * direction.y;
            var z = range.z * direction.z;
            return new Vector3(x, y, z);
        }
        
        #endregion

        /// Example use: shuffledArray = RandomStatics.ShuffleArray(startingArray);
        /// Where startingArray and shuffledArray are arrays of the same type
        public static T[] ShuffleArray<T>(T[] values) { return values.OrderBy(a => Random.Range(0f, 1f)).ToArray(); }
        
        /// Example use: shuffledList = RandomStatics.ShuffleList(startingList);
        /// Where startingList and shuffledList are lists of the same type
        public static List<T> ShuffleList<T>(List<T> values) { return values.OrderBy(a => Random.Range(0f, 1f)).ToList(); }
    }

    [Serializable] public struct MinMaxVector3
    {
        public Vector3 min;
        public Vector3 max;
    }
}
