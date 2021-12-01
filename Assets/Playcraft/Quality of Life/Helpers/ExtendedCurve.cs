using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class ExtendedCurve
    {
        [SerializeField] AnimationCurve curve;
        [Tooltip("Remaps input. Broaden this range to increase resolution.")]
        [SerializeField] Vector2 xRange;
        [Tooltip("Remaps output to Min(X)/Max(Y)")]
        [SerializeField] Vector2 yRange;
        
        public float Evaluate(float x)
        {
            var xPercent = Mathf.InverseLerp(xRange.x, xRange.y, x);
            var yPercent = curve.Evaluate(xPercent);
            return Mathf.Lerp(yRange.x, yRange.y, yPercent);
        }
    }
}