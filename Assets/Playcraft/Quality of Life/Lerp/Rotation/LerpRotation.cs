using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpRotation
    {
        public Transform self;
        public bool useLocal = true;
        
        Quaternion start;
        Quaternion end;
        
        public void SetSelfIfNull(Transform value) { if (!self) self = value; }
        
        Quaternion _rotation;

        public void Input(float percent)
        {
            _rotation = Quaternion.Slerp(start, end, percent);
            if (useLocal) self.localRotation = _rotation;
            else self.rotation = _rotation;
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
