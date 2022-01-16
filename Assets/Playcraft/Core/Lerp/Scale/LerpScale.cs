using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpScale
    {
        public Transform self;
        
        public Vector3 start;
        public Vector3 end; 
        public bool reverse;
        [SerializeField] bool useCurve;
        [SerializeField] AnimationCurve curve;

        [Range(0, 1)] public float xAnchor = 0.5f;
        [Range(0, 1)] public float yAnchor = 0.5f;
        [Range(0, 1)] public float zAnchor = 0.5f;

        Vector3 priorScale;
        Vector3 scaleStep => self.localScale - priorScale;

        public void SetSelfIfNull(Transform value) { if (!self) self = value; }
        
        /// Call continuously to move over time
        public void Input(float percent)
        {
            if (reverse) percent = 1f - percent;
            if (useCurve) percent = curve.Evaluate(percent);
            
            priorScale = self.localScale;
            self.localScale = Vector3.Lerp(start, end, percent);
                
            if (xAnchor != 0.5f) self.position += (.5f - xAnchor) * scaleStep.x * Vector3.right;
            if (yAnchor != 0.5f) self.position += (.5f - yAnchor) * scaleStep.y * Vector3.up;
            if (zAnchor != 0.5f) self.position += (.5f - zAnchor) * scaleStep.z * Vector3.forward;
        }
        
        public void SetNewScale(Vector3 value)
        {
            start = self.localScale;
            end = value;
            priorScale = start;
        }
    }
}
