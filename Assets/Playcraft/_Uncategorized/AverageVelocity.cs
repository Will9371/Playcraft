using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] 
    class AverageVelocity
    {
        [SerializeField] 
        int runningAverageLength;
        [Tooltip("1f = no effect, 0.5f = 50% cutoff per tick, 0f = full attenuation in one frame")]
        [SerializeField] [Range(0, 1)] 
        float attenuationFactor = 1f;
        [SerializeField]
        float projectionTime = .2f;
    
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
            points = new VelocityPoint[runningAverageLength];
        
            for (int i = 0; i < points.Length; i++)
                points[i] = new VelocityPoint(attenuationFactor);
        }
        
        public void FixedUpdate(Vector3 newPosition)
        {
            if (points == null || runningAverageLength != points.Length)
                Initialize();
                    
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
            
            if (overwriteIndex >= points.Length)
                overwriteIndex = 0;
        }
        
        float inverseAttenuation;
        float attenuationMultiplier;
                
        void SetAverageVector()
        {
            averageDelta = Vector3.zero;
        
            foreach (var point in points)
                averageDelta += point.delta;
            
            averageDelta /= points.Length;
            
            inverseAttenuation = 1 / (1 - attenuationFactor + .01f);
            attenuationMultiplier = 1 + (points.Length - 1) / (inverseAttenuation + .01f);
            averageDelta *= attenuationMultiplier;
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