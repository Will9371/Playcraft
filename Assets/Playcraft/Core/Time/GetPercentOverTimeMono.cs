using System.Collections;
using UnityEngine;

namespace Playcraft
{
    public class GetPercentOverTimeMono : MonoBehaviour
    {
        [SerializeField] ProgressEvent progress;
        [SerializeField] float duration;
        [SerializeField] FloatEvent Percent;
        [SerializeField] bool allowInterrupt = true;
        [SerializeField] bool invertOutputPercent;
        
        GetPercentOverTime timer = new GetPercentOverTime();
        float percent => timer.percent;
        float invertedPercent => 1 - timer.percent;
        float outputPercent => invertOutputPercent ? invertedPercent : percent;
        
        public void SetDurationAndBegin(float value)
        {
            SetDuration(value);
            Begin();
        }
        
        public void SetDuration(float value) { duration = value; }
        public float GetDuration() { return timer.duration; }
        
        public void Begin() 
        {
            //Debug.Log($"GetPercentOverTimeMono.Begin() {gameObject.name} {Time.time}", gameObject);
            if (gameObject.activeInHierarchy && (!timer.inProgress || allowInterrupt))
                StartCoroutine(Process()); 
        }
        
        IEnumerator Process()
        {
            if (progress) 
                progress.SetDurationAndBegin(duration);
            
            timer.SetDurationAndBegin(duration);
            
            while (timer.inProgress)
            {
                yield return null;
                Percent.Invoke(outputPercent);
            }
        }
        
        public void Cancel() 
        { 
            StopAllCoroutines(); 
            progress.Cancel();
        }
    }
}
