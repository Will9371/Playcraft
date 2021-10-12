using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpRotation : IPercent
    {
        public Transform self;
        public bool useLocal = true;
        [SerializeField] bool useCurve;
        [SerializeField] AnimationCurve curve;
        
        [SerializeField] Quaternion start;
        [SerializeField] Quaternion end;

        public void SetSelfIfNull(Transform value) { if (!self) self = value; }
        
        Quaternion _rotation;
        
        float _percent;
        float curvedPercent;
        public float percent
        {
            get => _percent;
            set
            {
                _percent = value;
                curvedPercent = useCurve ? curve.Evaluate(_percent) : _percent;
                _rotation = Quaternion.Slerp(start, end, curvedPercent);
                if (useLocal) self.localRotation = _rotation;
                else self.rotation = _rotation;                
            }
        }

        public void SetPath(Vector3 startVector, Vector3 endVector)
        {
            SetPath(Quaternion.Euler(startVector), Quaternion.Euler(endVector));
        }
        
        public void SetPath(Quaternion newStart, Quaternion newEnd)
        {
            start = newStart;
            end = newEnd;
        }
    }
}
