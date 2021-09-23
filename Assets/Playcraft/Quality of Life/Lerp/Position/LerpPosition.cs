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
        
        public Vector3 position
        {
            get => useLocal ? self.localPosition : self.position;
            set
            {
                if (useLocal) self.localPosition = value;
                else self.position = value;
            }
        }
        
        public float currentPercent => reverse ? 
            VectorMath.InverseLerp(end, start, position) : 
            VectorMath.InverseLerp(start, end, position);
        
        public void SetSelfIfNull(Transform value) { if (self == null) self = value; }
        
        // * Verify works for all assets set to true
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
        
        /// Call continuously to move over time
        public void Input(float percent)
        {
            if (reverse) percent = 1f - percent;
            position = Vector3.Lerp(start, end, percent);
        }
        
        public void SwitchDirection() { reverse = !reverse; }
        
        
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