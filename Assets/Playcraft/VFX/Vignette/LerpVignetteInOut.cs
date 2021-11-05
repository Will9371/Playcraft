using System.Collections;
using UnityEngine;

namespace Playcraft
{
    public class LerpVignetteInOut : MonoBehaviour
    {
        [SerializeField] AccessVignette vignette;
        [SerializeField] float fullDuration = 1f;
        [SerializeField] [Range(0, 1)] float maxIntensity;

        LerpFloat apply = new LerpFloat();
        LerpFloat unapply = new LerpFloat();
        LerpFloat activeStage = new LerpFloat();
        GetPercentOverTime timer = new GetPercentOverTime();
        
        const float stageCount = 2f;
        float stageDuration => fullDuration / stageCount;
        
        float percent { set => vignette.intensity = activeStage.Input(value); }
        
        // Only enabled when Process is running for efficiency
        void Update() { percent = activeStage.percent; }
        
        public void Begin(float duration, Color color)
        {
            vignette.color = color;
            Begin(duration);
        }

        public void Begin(float duration) 
        { 
            fullDuration = duration; 
            Begin(); 
        }
        
        public void Begin() { StartCoroutine(Process()); }
        
        IEnumerator Process()
        {
            enabled = true;
            yield return timer.Run(activeStage = apply, stageDuration); 
            yield return timer.Run(activeStage = unapply, stageDuration); 
            enabled = false;
        }

        void OnValidate()
        {
            apply.start = 0;
            apply.end = maxIntensity;
            unapply.start = maxIntensity;
            unapply.end = 0;
        }
    }
}