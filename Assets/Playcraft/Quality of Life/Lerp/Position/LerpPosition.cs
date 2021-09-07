using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpPosition
    {
        public Transform self;
        public bool useLocal = true;

        public Vector3 start;
        public Vector3 end;
        public bool reverse;
        
        public void SetSelfIfNull(Transform value) { if (self == null) self = value; }
        
        public void SetStartAtSelf() { start = self.position; }
        
        public void SetPath(Vector3 newStart, Vector3 newEnd)
        {
            start = newStart;
            end = newEnd;
        }
        
        public void SetEnd(Vector3 newEnd)
        {
            start = end;
            end = newEnd;
        }
        
        Vector3 _position;

        /// Call continuously to move over time
        public void Input(float percent)
        {
            if (reverse) percent = 1f - percent;
            
            _position = Vector3.Lerp(start, end, percent);
            if (useLocal) self.localPosition = _position;
            else self.position = _position;
        }
        
        public void SwitchDirection() { reverse = !reverse; }
    }
}