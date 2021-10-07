using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] class AverageVelocity
    {
        [SerializeField] int runningAverageLength;
        [Tooltip("1f = no effect, 0.5f = 50% cutoff per tick, 0f = full attenuation in one frame")]
        [SerializeField] [Range(0, 1)] float attenuationFactor = 1f;
    
        [HideInInspector] public Vector3 averageDelta;
        VelocityPoint[] points;
        
        VelocityPoint currentPoint => points[overwriteIndex];
        
        int overwriteIndex;

        public void Validate()
        {
            if (points == null || runningAverageLength != points.Length)
                Initialize();
        }
        
        void Initialize()
        {
            points = new VelocityPoint[runningAverageLength];
        
            for (int i = 0; i < points.Length; i++)
                points[i] = new VelocityPoint(attenuationFactor);
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