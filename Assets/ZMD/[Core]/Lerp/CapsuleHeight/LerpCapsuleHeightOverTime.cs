using System;
using System.Collections;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class LerpCapsuleHeightOverTime : ILerpOverTime
    {
        public float duration;
        GetPercentOverTime timer = new GetPercentOverTime();
            
        public LerpCapsuleHeight process;
            
        public IEnumerator Run() { yield return timer.Run(process, duration); }
        public void FlipPath() { process.FlipPath(); }
        public void FlipAndRun(MonoBehaviour mono) { FlipPath(); mono.StartCoroutine(Run()); }
            
        public float percent { get => process.percent; set => process.percent = value; }
        public AnimationCurve curve { get => process.curve; set => process.SetCurve(value); }
        public bool useCurve { get => process.useCurve; set => process.useCurve = value; }
        public void SetDuration(float value) { duration = value; }

        public void Interrupt() { timer.interruptFlag = true; }
    }
}