using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class GetPercentOverTime
    {
        public float duration;
        float startTime;
        
        public float elapsedTime => Time.time - startTime;
        public float percent => elapsedTime/duration;
        public bool inProgress => percent < 1f;
        
        public void SetDurationAndBegin(float newDuration)
        {
            duration = newDuration;
            Begin();
        }
        
        public void Begin() { startTime = Time.time; }

        public (float, bool) GetProgress() { return (percent, !inProgress); }
    }
}
