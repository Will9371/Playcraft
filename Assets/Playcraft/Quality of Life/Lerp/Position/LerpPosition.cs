using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpPosition : IPercent
    {
        public Transform self;
        public bool useLocal = true;
        public bool useCurve;
        public AnimationCurve curve;

        public Vector3 start;
        public Vector3 end;
        public bool reverse;
        
        public float distance => Vector3.Distance(start, end);
        
        public Vector3 position
        {
            get => useLocal ? self.localPosition : self.position;
            set
            {
                if (!self) return;
                if (useLocal) self.localPosition = value;
                else self.position = value;
            }
        }
        
        public float currentPercent => reverse ? 
            VectorMath.InverseLerp(end, start, position) : 
            VectorMath.InverseLerp(start, end, position);
        
        public void SetSelfIfNull(Transform value) { if (self == null) self = value; }
        
        public void Initialize() 
        { 
            start = position;
            end = position;
        }
        
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
        
        float _percent;
        public float curvedPercent;
        public float percent
        {
            get => _percent;
            set
            {
                _percent = reverse ? 1f - value : value;
                curvedPercent = useCurve ? curve.Evaluate(_percent) : _percent;
                position = Vector3.Lerp(start, end, curvedPercent);
            }
        }
        
        public void SetCurve(bool useCurve, AnimationCurve curve) 
        { 
            this.useCurve = useCurve;
            if (curve != null) this.curve = new AnimationCurve(curve.keys); 
        }

        // * Consider removal
        public void SwitchDirection() { reverse = !reverse; }

        // * Consider removal
        #region Cached Path
        
        bool cacheInitialized;
        Vector3 cachedStart;
        Vector3 cachedEnd;
        
        public void CachePath()
        {
            cacheInitialized = true;
            cachedStart = start;
            cachedEnd = end;
        }
        
        public void SetPathToStartFromSelf()
        {
            if (!cacheInitialized) return;
            start = position;
            end = cachedStart;
        }
        
        public void ResetPathFromCache()
        {
            if (!cacheInitialized) return;
            start = cachedStart;
            end = cachedEnd;
        }
        
        #endregion
    }
}