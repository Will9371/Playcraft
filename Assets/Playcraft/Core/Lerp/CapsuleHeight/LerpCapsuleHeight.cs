using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpCapsuleHeight
    {
        public CapsuleCollider capsule;
        [Range(0, 1)] public float yAnchor = 0.5f;
        [SerializeField] bool useCurve;
        [SerializeField] AnimationCurve curve;

        public float start;
        public float end;
        public bool reverse;

        float priorHeight;
        float heightStep => capsule.height - priorHeight;
        
        public void SetCapsuleIfNull(CapsuleCollider value) { if (!capsule) capsule = value; }

        // Call continuously to move over time
        public void Input(float percent)
        {
            if (reverse) percent = 1f - percent;
            if (useCurve) percent = curve.Evaluate(percent);
            priorHeight = capsule.height;
            capsule.height = Mathf.Lerp(start, end, percent);
            capsule.transform.position += (.5f - yAnchor) * heightStep * Vector3.up;
        }
    }
}
