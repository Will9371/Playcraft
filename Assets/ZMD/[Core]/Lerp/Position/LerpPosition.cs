using System;
using UnityEngine;

namespace ZMD
{
    [Serializable] public class LerpPosition : IPercent
    {
        public LerpLocationData data;
        
        public Transform self { get => data.self; set => data.self = value; }
        public bool useLocal => data.useLocal;
        public bool useCurve { get => data.useCurve; set => data.useCurve = value; }
        public AnimationCurve curve { get => data.curve; set => data.SetCurve(value); }

        public Vector3 start;
        public Vector3 end;
        
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
        
        public float currentPercent => VectorMath.InverseLerp(start, end, position);

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
        
        public void FlipPath()
        {
            var _start = start;
            start = end;
            end = _start;
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
                _percent = value;
                curvedPercent = useCurve ? curve.Evaluate(_percent) : _percent;
                position = Vector3.Lerp(start, end, curvedPercent);
            }
        }
    }
}

#region Reverse Direction (obsolete, implement on higher abstraction)
/*public void SetCurve(bool useCurve, AnimationCurve curve) 
{ 
    this.useCurve = useCurve;
    if (curve != null) this.curve = new AnimationCurve(curve.keys); 
}*/

// * Consider removal
/*
#region Cached Path

//public void SwitchDirection() { reverse = !reverse; }

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
*/
#endregion