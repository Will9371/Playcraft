using System;
using UnityEngine;


namespace Playcraft
{
    /// Move position and rotation (TBD: include scale).
    /// Uses static locations (will not update as a reference transform moves).
    [Serializable] public class LerpLocation : IPercent
    {
        public Transform self;
        public bool useLocal;
        [SerializeField] bool useCurve;
        [SerializeField] AnimationCurve curve;

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
                
                self.position = position.position;
                self.rotation = rotation.rotation;
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
            position.end = endPosition;
            rotation.end = endRotation;
        }

        public void OnValidate()
        {
            position.self = self;
            rotation.self = self;
            position.useLocal = useLocal;
            rotation.useLocal = useLocal;
            position.useCurve = useCurve;
            rotation.useCurve = useCurve;
            position.curve = curve;
            rotation.curve = curve;
        }
    }
}