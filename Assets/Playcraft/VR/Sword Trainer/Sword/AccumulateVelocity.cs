using System;
using UnityEngine;

namespace Playcraft
{
    public class AccumulateVelocity : MonoBehaviour
    {
        [SerializeField] int runningAverageLength;
        [Tooltip("1f = no effect, 0.5f = 50% cutoff per tick")]
        [SerializeField] [Range(0, 1)] float attenuationFactor = 1f;
            
        VelocityAverage runningAverage;
        
        public Vector3 averageVelocity;
        public float averageMagnitude;
        
        public Vector3 averageDirection => averageVelocity.normalized;
        public float edgeAlignment => Mathf.Abs(Vector3.Dot(transform.forward, averageDirection));
        
        void Start() { Initialize(); }
        
        int priorRunningAverageLength;
        
        void OnValidate()
        {
            if (priorRunningAverageLength != runningAverageLength)
                Initialize();
        }
        
        void Initialize()
        {
            runningAverage = new VelocityAverage(runningAverageLength, attenuationFactor);
            priorRunningAverageLength = runningAverageLength;     
        }
        
        void FixedUpdate()
        {
            runningAverage.Update(transform.position);
            
            averageVelocity = runningAverage.averageDelta;
            averageMagnitude = averageVelocity.magnitude;
        }
        
        [Serializable] 
        class VelocityAverage
        {
            public Vector3 averageDelta;    
            VelocityPoint[] points;
            
            int overwriteIndex;
            VelocityPoint currentPoint => points[overwriteIndex];
            
            public VelocityAverage(int length, float attenuation)
            {
                points = new VelocityPoint[length];
                
                for (int i = 0; i < points.Length; i++)
                    points[i] = new VelocityPoint(attenuation);
            }
            
            public void Update(Vector3 newPosition)
            {            
                foreach (var point in points)
                    point.Attenuate();
            
                RefreshCurrentPoint(newPosition);    
                SetAverageVector();
            }
            
            void RefreshCurrentPoint(Vector3 newPosition)
            {
                currentPoint.Refresh(newPosition);
                overwriteIndex++;
                
                if (overwriteIndex >= points.Length)
                    overwriteIndex = 0;
            }
                    
            void SetAverageVector()
            {
                averageDelta = Vector3.zero;
            
                foreach (var point in points)
                    averageDelta += point.delta;
                
                averageDelta /= points.Length;
            }
            
            class VelocityPoint
            {
                Vector3 position;
                public Vector3 delta;
                
                float attenuation;
            
                public VelocityPoint(float attenuation = 1f)
                {
                    this.attenuation = attenuation;
                }
            
                public void Refresh(Vector3 newPosition) 
                { 
                    delta = newPosition - position; 
                    position = newPosition;            
                }
                
                public void Attenuate() { delta *= attenuation; }
            }
        }
    }
}
