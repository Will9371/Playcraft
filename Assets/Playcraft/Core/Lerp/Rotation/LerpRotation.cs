using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] 
    public class LerpRotation : IPercent
    {
        public Transform self;
        public bool useLocal = true;
        public bool useCurve;
        public AnimationCurve curve;
        
        public Quaternion start;
        public Quaternion end;

        public void SetSelfIfNull(Transform value) { if (!self) self = value; }
        
        [NonSerialized]
        public Quaternion rotation;
        
        float _percent;
        float curvedPercent;
        public float percent
        {
            get => _percent;
            set
            {
                _percent = value;
                curvedPercent = useCurve ? curve.Evaluate(_percent) : _percent;
                rotation = Quaternion.Slerp(start, end, curvedPercent);
                if (useLocal) self.localRotation = rotation;
                else self.rotation = rotation;                
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
        
        public void SetCurve(bool useCurve, AnimationCurve curve) 
        { 
            this.useCurve = useCurve;
            if (curve != null) this.curve = new AnimationCurve(curve.keys); 
        }
    }
}
