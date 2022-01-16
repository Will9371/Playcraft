using System;
using System.Collections;
using UnityEngine;

namespace Playcraft
{
    [Serializable] 
    public class GetPercentOverTime
    {
        public float duration = 1f;
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
        
        public IEnumerator Run(IPercent process, float newDuration = -1f)
        {
            if (newDuration > 0f)
                duration = newDuration;
            
            startTime = Time.time;

            while (inProgress)
            {
                process.percent = percent;
                yield return null;
            }
            
            process.percent = 1f;
        }
        
        public IEnumerator Run(IPercent[] processes, float newDuration = -1f)
        {
            if (newDuration > 0f)
                duration = newDuration;
            
            startTime = Time.time;

            while (inProgress)
            {
                foreach (var process in processes)
                    process.percent = percent;
                    
                yield return null;
            }
            
            foreach (var process in processes)
                process.percent = 1f;
        }
    }
}
