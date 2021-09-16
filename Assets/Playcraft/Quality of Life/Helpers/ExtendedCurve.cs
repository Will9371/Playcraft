using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class ExtendedCurve
    {
        [SerializeField] AnimationCurve curve;
        [SerializeField] Vector2 xRange;
        [SerializeField] Vector2 yRange;
        
        public float Evaluate(float x)
        {
            var xPercent = Mathf.InverseLerp(xRange.x, xRange.y, x);
            var yPercent = curve.Evaluate(xPercent);
            return Mathf.Lerp(yRange.x, yRange.y, yPercent);
        }
    }
}