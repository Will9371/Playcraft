using System;
using System.Collections;
using UnityEngine;

namespace ZMD
{
    public class LerpScaleOverTimeMono : MonoBehaviour
    {
        [SerializeField] LerpScaleOverTime process;
        public void Begin() { StartCoroutine(process.Run()); }
        public void Interrupt() { process.Interrupt(); }
    }
    
    [Serializable]
    public class LerpScaleOverTime : ILerpOverTime
    {
        public float duration;
        GetPercentOverTime timer = new GetPercentOverTime();
        
        public LerpScale process;
        
        public IEnumerator Run() { yield return timer.Run(process, duration); }
        public void FlipPath() { process.FlipPath(); }
        public void FlipAndRun(MonoBehaviour mono) { FlipPath(); mono.StartCoroutine(Run()); }
        
        public LerpScaleData data { get => process.data; set => process.data = value; }
        public float percent { get => process.percent; set => process.percent = value; }
        public bool useCurve { get => data.useCurve; set => data.useCurve = value; }
        public AnimationCurve curve { get => data.curve; set => data.SetCurve(value); }
        public void SetDuration(float value) { duration = value; }

        public void SetScales(Vector3 start, Vector3 end)
        {
            process.start = start;
            process.end = end;
        }
        
        public void Interrupt() { timer.interruptFlag = true; }
    }
}
