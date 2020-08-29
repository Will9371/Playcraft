using UnityEngine;

namespace Playcraft
{
    public static class RandomStatics
    {
        public static int RandomNoRepeat(int min, int max, int prior)
        {
            if (max - min < 1)
                return prior;
            
            int result = prior;
            
            while (result == prior)
                result = Random.Range(min, max);
            
            return result;
        }
        
        public static Vector3 RandomInRange(MinMaxVector3 range)
        {
            return RandomInRange(range.minimum, range.maximum);
        }
        
        public static Vector3 RandomInRange(Vector3 min, Vector3 max)
        {
            var x = Random.Range(min.x, max.x);
            var y = Random.Range(min.y, max.y);
            var z = Random.Range(min.z, max.z);
            return new Vector3(x, y, z);
        }
    }
}
