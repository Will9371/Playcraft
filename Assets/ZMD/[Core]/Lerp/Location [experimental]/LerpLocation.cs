using System;
using UnityEngine;


namespace ZMD
{
    /// Move position and rotation (TBD: include scale).
    /// Uses static locations (will not update as a reference transform moves).
    [Serializable] 
    public class LerpLocation : IPercent
    {
        public LerpLocationData data;
        
        public Transform self => data.self;
        public bool useLocal => data.useLocal;
        public bool useCurve => data.useCurve;
        public AnimationCurve curve => data.curve;

        public LerpPosition position;
        public LerpRotation rotation;
        
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
                
                if (useLocal)
                {
                    self.localPosition = position.position;
                    self.localRotation = rotation.rotation;
                }
                else
                {
                    self.position = position.position;
                    self.rotation = rotation.rotation;
                }
            }
        }
        
        public void SetStart(Location location) { SetStart(location.position, location.rotation); }
        public void SetStart(Vector3 position, Quaternion rotation) 
        {
            this.position.start = position;
            this.rotation.start = rotation; 
        }

        public void SetEnd(Location location) { SetEnd(location.position, location.rotation); }
        public void SetEnd(Vector3 position, Quaternion rotation) 
        {
            this.position.end = position;
            this.rotation.end = rotation; 
        }

        /// Sets start to current end, takes in new values for end
        public void SetNewEnd(Vector3 position, Quaternion rotation) 
        {
            SetStart(this.position.end, this.rotation.end);
            SetEnd(position, rotation);
        }

        public void SetSelfToEnd(Transform newEnd) { SetSelfToEnd(new Location(newEnd)); }
        
        public void SetSelfToEnd(Location location) { SetSelfToEnd(location.position, location.rotation); }
        
        public void SetSelfToEnd(Vector3 endPosition, Quaternion endRotation)
        {
            SetStart(position.position, rotation.rotation);
            SetEnd(endPosition, endRotation);
        }

        public void OnValidate()
        {
            position.data = data;
            rotation.data = data;
        }
    }
    
    [Serializable]
    public class LerpLocationData
    {
        public Transform self;
        public bool useLocal;
        public bool useCurve;
        public AnimationCurve curve;
        
        public LerpLocationData(LerpLocationData original, Transform uniqueSelf = null)
        {
            self = uniqueSelf ? uniqueSelf : original.self;
            useLocal = original.useLocal;
            useCurve = original.useCurve;
            
            if (original.curve != null)
                SetCurve(original.curve);
        }
        
        public void SetCurve(AnimationCurve curve) { this.curve = new AnimationCurve(curve.keys); }
    }
}