using UnityEngine;

namespace Playcraft
{
    public class CalculateParabolicArc
    {
        float range = 5f;
        float gravity = 9.8f;
        int resolution = 40;
        float maxDrop = 5f;
        
        public CalculateParabolicArc(float range, float gravity, int resolution, float maxDrop)
        {
            this.range = range;
            this.gravity = gravity;
            this.resolution = resolution;
            this.maxDrop = maxDrop;
        }
        
        float radianAngle;
        Vector3[] arcArray;

        public Vector3[] Calculate(float angle)
        {        
            radianAngle = angle * Mathf.Deg2Rad;
            arcArray = new Vector3[resolution + 1];

            float maxDistance = ((range * Mathf.Cos(radianAngle)) / gravity) * 
                                ((range * Mathf.Sin(radianAngle) + Mathf.Sqrt(Mathf.Pow(range * 
                                  Mathf.Sin(radianAngle), 2) + (2 * gravity * maxDrop))));

            float percent;
            for (int j = 0; j <= resolution; j++)
            {
                percent = j / (float)resolution; 
                arcArray[j] = CalculateArcPoint(percent, maxDistance);
            }
            
            return arcArray;
        }

        private Vector3 CalculateArcPoint(float t, float maxDistance)
        {
            float z = t * maxDistance;
            float y = z * Mathf.Tan(radianAngle) - ((gravity * z * z) / 
                      (2 * range * range * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

            return new Vector3(0f, y, z);
        }
    }
}