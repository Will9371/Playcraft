using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] 
    public class AverageVelocity
    {
        [SerializeField] 
        int runningAverageLength;
        [Tooltip("1f = no effect, 0.5f = 50% cutoff per tick, 0f = full attenuation in one frame")]
        [SerializeField] [Range(0, 1)] 
        float attenuationFactor = 1f;
        [SerializeField]
        float projectionTime = .2f;
        
        public void SetData(AverageVelocitySO data)
        {
            runningAverageLength = data.runningAverageLength;
            attenuationFactor = data.attenuationFactor;
            projectionTime = data.projectionTime;
            Initialize();
        }
    
        [HideInInspector] 
        public Vector3 averageDelta;
        
        public float averageMagnitude => averageDelta.magnitude;
        public Vector3 averageDirection => averageDelta.normalized;
        
        [HideInInspector]
        public Vector3 projectedPosition;
        
        public int pointCount => points.Length;

        VelocityPoint[] points;
        VelocityPoint currentPoint => points[overwriteIndex];
        
        public float EdgeAlignment(Vector3 forward) { return Mathf.Abs(Vector3.Dot(forward, averageDirection)); }
        
        int overwriteIndex;

        void Initialize()
        {
            overwriteIndex = 0;
            points = new VelocityPoint[runningAverageLength];
        
            for (int i = 0; i < pointCount; i++)
                points[i] = new VelocityPoint(attenuationFactor);
        }
        
        /// Frame rate must be constant for a consistently weighted running average
        public void FixedUpdate(Vector3 newPosition)
        {
            if (points == null || runningAverageLength != pointCount)
                Initialize();
                    
            if (attenuationFactor < 1f)
                foreach (var point in points)
                    point.Attenuate();
        
            RefreshCurrentPoint(newPosition);    
            SetAverageVector();
            
            projectedPosition = newPosition + (averageDelta * projectionTime) / Time.fixedDeltaTime;
        }
        
        void RefreshCurrentPoint(Vector3 newPosition)
        {
            currentPoint.Refresh(newPosition);
            overwriteIndex++;
            
            if (overwriteIndex >= pointCount)
                overwriteIndex = 0;
        }
        
        float inverseAttenuation;
        float attenuationMultiplier;
                
        void SetAverageVector()
        {
            averageDelta = Vector3.zero;
        
            foreach (var point in points)
                averageDelta += point.delta;
            
            averageDelta /= Mathf.Pow(pointCount, 2);
            
            if (attenuationFactor < 1f)
            {
                inverseAttenuation = 1 / (1 - attenuationFactor + .001f);
                attenuationMultiplier = 1 + (pointCount - 1) / (inverseAttenuation + .001f);
                averageDelta *= attenuationMultiplier;
            }
        }
        
        class VelocityPoint
        {
            Vector3 position;
            public Vector3 delta;
            
            float attenuation;
        
            public VelocityPoint(float attenuation = 1f) { this.attenuation = attenuation; }
        
            public void Refresh(Vector3 newPosition) 
            { 
                delta = newPosition - position; 
                position = newPosition;            
            }
            
            public void Attenuate() { delta *= attenuation; }
        }
    }
}