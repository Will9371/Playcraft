using System;
using UnityEngine;

// Moves position and rotation to blend between Transform references
namespace Playcraft
{
    [Serializable]
    public class LerpTransform : IPercent
    {
        public Transform self;
        public bool useLocal;
        public bool useCurve;
        public AnimationCurve curve;

        public Transform start;
        public Transform end;

        LerpPosition position = new LerpPosition();
        LerpRotation rotation = new LerpRotation();
        
        float _percent;
        float curvedPercent;
        public float percent
        {
            get => _percent;
            set
            {
                _percent = value;
                curvedPercent = useCurve ? curve.Evaluate(_percent) : _percent;
                
                rotation.percent = curvedPercent;
                position.percent = curvedPercent;
            }
        }

        public void SetStart(Transform value) 
        {
            start = value;
            if (!value) return;
            
            position.start = useLocal ? value.localPosition : value.position;
            rotation.start = useLocal ? value.localRotation : value.rotation; 
        }

        public void SetEnd(Transform value)
        {
            end = value;
            if (!value) return;
            
            position.end = useLocal ? value.localPosition : value.position;
            rotation.end = useLocal ? value.localRotation : value.rotation;
        }
        
        public void SetStartAndEnd(Transform start, Transform end) { SetStart(start); SetEnd(end); }
        
        public void SetNewEnd(Transform value) { SetStart(end); SetEnd(value); }
        
        public void SetSelfToEnd(Transform end) { SetStart(self); SetEnd(end); }
        
        public void SetSelfToEnd(Vector3 endPosition, Quaternion endRotation)
        {
            SetStart(self);
            position.end = endPosition;
            rotation.end = endRotation;
        }
        
        public void RefreshEndpoints() { SetStartAndEnd(start, end); }

        public void OnValidate()
        {
            position.self = self;
            rotation.self = self;
            position.useLocal = useLocal;
            rotation.useLocal = useLocal;
            position.SetCurve(useCurve, curve);
            rotation.SetCurve(useCurve, curve);
            SetStart(start);
            SetEnd(end);
        }
        
        // Will propagate down to position and rotation on next OnValidate call
        public void SetCurve(bool useCurve, AnimationCurve curve) 
        { 
            this.useCurve = useCurve;
            if (curve != null) this.curve = new AnimationCurve(curve.keys); 
        }
    }
}
