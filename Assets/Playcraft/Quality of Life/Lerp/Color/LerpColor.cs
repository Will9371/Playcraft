using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpColor
    {
        public Color start;
        public Color end;
        public bool reverse;
        [SerializeField] bool useCurve;
        [SerializeField] AnimationCurve curve;
        
        public ColorEvent Output;

        public void Input(float percent) 
        { 
            if (reverse) percent = 1f - percent;
            if (useCurve) percent = curve.Evaluate(percent);
            Output.Invoke(Color.Lerp(start, end, percent));
        }
        
        public void SetTargetColor(Color targetColor)
        {
            start = end;
            end = targetColor;
        }
    }
}
