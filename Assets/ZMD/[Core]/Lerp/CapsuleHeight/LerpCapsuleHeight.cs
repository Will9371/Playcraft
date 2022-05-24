using System;
using UnityEngine;

namespace ZMD
{
    [Serializable] public class LerpCapsuleHeight : IPercent
    {
        public CapsuleCollider capsule;
        [Range(0, 1)] public float yAnchor = 0.5f;
        public bool useCurve;
        public AnimationCurve curve;

        public float start;
        public float end;
        public bool reverse;

        float priorHeight;
        float heightStep => capsule.height - priorHeight;
        
        public void SetCapsuleIfNull(CapsuleCollider value) { if (!capsule) capsule = value; }
        
        public void FlipPath() { reverse = !reverse; }

        // Call continuously to move over time
        public void Input(float value) { this.percent = value; }

        public float percent 
        { 
            get => capsule.height; 
            set
            {
                var newValue = value; 
                if (reverse) newValue = 1f - newValue;
                if (useCurve) newValue = curve.Evaluate(newValue);
                
                priorHeight = capsule.height;
                capsule.height = Mathf.Lerp(start, end, newValue);
                capsule.transform.position += (.5f - yAnchor) * heightStep * Vector3.up;                
            }
        }
        
        public void SetCurve(AnimationCurve value) { curve = new AnimationCurve(value.keys); }
    }
}
