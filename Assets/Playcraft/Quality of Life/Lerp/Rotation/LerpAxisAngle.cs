using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class LerpAxisAngle : IPercent
    {
        public RotateAxis axis;
        public bool useCurve;
        public AnimationCurve curve;
        
        float startAngle;
        float endAngle;
        float angle;
        
        float _percent;
        public float curvedPercent { get; private set; }
        public float percent 
        { 
            get => _percent; 
            set
            {
                _percent = value;
                curvedPercent = useCurve ? curve.Evaluate(_percent) : _percent;
                angle = Mathf.Lerp(startAngle, endAngle, curvedPercent);
                axis.SetAngle(angle);
            }
        }

        public void SetDestination(float value)
        {
            startAngle = endAngle;
            endAngle = value;
        }
        
        public void SetEnds(float start, float end)
        {
            startAngle = start;
            endAngle = end;
        }
        
        public void SetCurve(bool useCurve, AnimationCurve curve) 
        { 
            this.useCurve = useCurve;
            if (curve != null) this.curve = new AnimationCurve(curve.keys); 
        }
        
        public void SetAngle(float value) { axis.SetAngle(value); endAngle = value; }
        public void Validate() { axis.ValidateAngle(); }
    }
}
