using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class LerpFloat : IPercent
    {
        [SerializeField] bool useCurve;
        [SerializeField] AnimationCurve curve;
        public float start;
        public float end;
        public float result;

        float _percent;
        public float curvedPercent { get; private set; }
        public float percent 
        { 
            get => _percent; 
            set
            {
                _percent = value;
                curvedPercent = useCurve ? curve.Evaluate(_percent) : _percent;
                result = Mathf.Lerp(start, end, curvedPercent);
            }
        }
        
        public float Input(float percent) { this.percent = percent; return result; }
    }
}
