using System;
using UnityEngine;

namespace ZMD
{
    [Serializable] public class LerpScale : IPercent
    {
        public LerpScaleData data;
    
        public Transform self => data.self;
        public bool useCurve => data.useCurve;
        public AnimationCurve curve { get => data.curve; set => data.SetCurve(value); }
        public float xAnchor => data.xAnchor;
        public float yAnchor => data.yAnchor;
        public float zAnchor => data.zAnchor;        
        
        public Vector3 start;
        public Vector3 end;
        
        Vector3 priorScale;
        Vector3 scaleStep => self.localScale - priorScale;
        
        public Vector3 scale
        {
            get => self.localScale;
            set
            {
                if (!self) return;
                self.localScale = value;
            }
        }

        float _percent;
        public float curvedPercent;
        public float percent
        {
            get => _percent;
            set
            {
                _percent = value;
                curvedPercent = useCurve ? curve.Evaluate(_percent) : _percent;
                scale = Vector3.Lerp(start, end, curvedPercent);
                
                if (xAnchor != 0.5f) self.position += (.5f - xAnchor) * scaleStep.x * Vector3.right;
                if (yAnchor != 0.5f) self.position += (.5f - yAnchor) * scaleStep.y * Vector3.up;
                if (zAnchor != 0.5f) self.position += (.5f - zAnchor) * scaleStep.z * Vector3.forward;
            }
        }

        public void SetNewScale(Vector3 value)
        {
            start = self.localScale;
            end = value;
            priorScale = start;
        }
        
        public void FlipPath()
        {
            var _start = start;
            start = end;
            end = _start;
        }
    }
    
    [Serializable]
    public class LerpScaleData
    {
        public Transform self;
        public bool useCurve;
        public AnimationCurve curve;
        
        [Range(0, 1)] public float xAnchor;
        [Range(0, 1)] public float yAnchor;
        [Range(0, 1)] public float zAnchor;
        
        public LerpScaleData(LerpScaleData original, Transform uniqueSelf = null)
        {
            self = uniqueSelf ? uniqueSelf : original.self;
            
            useCurve = original.useCurve;
            SetCurve(original.curve);
            
            xAnchor = original.xAnchor;
            yAnchor = original.yAnchor;
            zAnchor = original.zAnchor;
        }
        
        public void SetCurve(AnimationCurve value) { curve = new AnimationCurve(value.keys); }
    }
}
