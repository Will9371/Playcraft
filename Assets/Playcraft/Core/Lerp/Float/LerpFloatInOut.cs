using System;
using System.Collections;
using UnityEngine;

namespace Playcraft
{    
    [Serializable]
    public class LerpFloatInOut
    {
        [SerializeField] float duration = 1f;
        [SerializeField] float holdTime = 0f;
        [SerializeField] float max;
        
        public float value => activeStage.result;
    
        LerpFloat apply = new LerpFloat();
        LerpFloat unapply = new LerpFloat();
        LerpFloat activeStage = new LerpFloat();
        GetPercentOverTime timer = new GetPercentOverTime();
            
        const float stageCount = 2f;
        float stageDuration => duration / stageCount;
        
        MonoBehaviour mono;

        public void Begin(MonoBehaviour mono, float duration = -1f, float holdTime = -1f) 
        {
            if (duration > 0f) this.duration = duration;
            if (holdTime > 0f) this.holdTime = holdTime;
        
            this.mono = mono; 
            this.mono.StartCoroutine(Process()); 
        }
        
        IEnumerator Process()
        {
            mono.enabled = true;
            yield return timer.Run(activeStage = apply, stageDuration); 
            yield return new WaitForSeconds(holdTime);
            yield return timer.Run(activeStage = unapply, stageDuration); 
            mono.enabled = false;
        }

        public void Validate()
        {
            apply.start = 0;
            apply.end = max;
            unapply.start = max;
            unapply.end = 0;
        }       
    }
}
