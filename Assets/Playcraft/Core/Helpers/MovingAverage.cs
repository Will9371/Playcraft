using UnityEngine;

// CONSIDER REFACTOR: use generics (if possible) to eliminate duplication
namespace Playcraft
{
    public class MovingAverageFloat
    {
        public MovingAverageFloat(int maxCount)
        {
            this.maxCount = maxCount;
        }
        
        int maxCount;
        int count;
        
        public float value;

        public void Update(float newValue)
        {
            if (count >= maxCount)
                value += (newValue - value) / (count + 1);
            else
            {
                count++;
                value += newValue;
                value /= count;
            }
        }
    }

    public class MovingAverageVector3
    {
        public MovingAverageVector3(int maxCount)
        {
            this.maxCount = maxCount;
        }
        
        int maxCount;
        int count;
        
        public Vector3 value;

        public void Update(Vector3 newValue)
        {
            if (count >= maxCount)
                value += (newValue - value) / (count + 1);
            else
            {
                count++;
                value += newValue;
                value /= count;
            }
        }
    }
}

