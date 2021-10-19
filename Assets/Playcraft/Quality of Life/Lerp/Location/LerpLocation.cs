using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class LerpLocation
    {
        public Transform self;
        [SerializeField] bool useLocal;
        [SerializeField] bool useCurve;
        [SerializeField] AnimationCurve curve;
        
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
            position.start = useLocal ? value.localPosition : value.position;
            rotation.start = useLocal ? value.localRotation : value.rotation; 
        }
        
        public void SetEnd(Transform value)
        {
            end = value;
            position.end = useLocal ? value.localPosition : value.position;
            rotation.end = useLocal ? value.localRotation : value.rotation;
        }
        
        public void SetStartAndEnd(Transform start, Transform end)
        {
            SetStart(start);
            SetEnd(end);
        }
        
        public void SetNewEnd(Transform value)
        {
            SetStart(end);
            SetEnd(value);
        }
        
        public void Validate()
        {
            position.self = self;
            rotation.self = self;
            position.useLocal = useLocal;
            rotation.useLocal = useLocal;
            position.useCurve = useCurve;
            rotation.useCurve = useCurve;
            position.curve = curve;
            rotation.curve = curve;
            SetStart(start);
            SetEnd(end);
        }
    }
}
