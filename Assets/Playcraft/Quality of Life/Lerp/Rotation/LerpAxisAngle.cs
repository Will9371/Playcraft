using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class LerpAxisAngle : IPercent
    {
        [SerializeField] RotateAxis axis;
        [SerializeField] bool useCurve;
        [SerializeField] AnimationCurve curve;
        
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
        
        public void SetAngle(float value) { axis.SetAngle(value); }
        public void Validate() { axis.ValidateAngle(); }
    }
}
