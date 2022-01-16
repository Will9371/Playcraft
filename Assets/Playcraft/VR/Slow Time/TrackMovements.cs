using System;
using System.Linq;
using UnityEngine;

// REFACTOR: use non-mono AverageVelocity
namespace Playcraft
{
    public class TrackMovements : MonoBehaviour
    {
        enum Calculation { Sum, Highest, Average }

        [SerializeField] Calculation calculation;
        [SerializeField] TrackedPoint[] trackedPoints;
        [SerializeField] FloatEvent output;
        
        float GetSpeed()
        {
            switch (calculation)
            {
                case Calculation.Sum: return trackedPoints.Sum(s => s.weightedSpeed);
                case Calculation.Highest: return GetHighestValue();
                case Calculation.Average: return trackedPoints.Average(s => s.weightedSpeed);
                default: return 1f;
            }
        }
        
        float GetHighestValue()
        {
            var values = new float[trackedPoints.Length];
            
            for (int i = 0; i < values.Length; i++)
                values[i] = trackedPoints[i].weightedSpeed;
                
            return Mathf.Max(values);
        }    

        void Update() { output.Invoke(GetSpeed()); }

        [Serializable]
        public class TrackedPoint
        {
            [SerializeField] AverageVelocityMono average;
            [SerializeField] [Range(0, 1)] float weight = 1f;
            
            public float weightedSpeed => average.averageMagnitude * weight;
        }
    }
}

