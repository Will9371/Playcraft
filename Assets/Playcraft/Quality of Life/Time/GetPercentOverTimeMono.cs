using System.Collections;
using UnityEngine;

// REFACTOR: separate (1) timer interface, (2) progress event wrapper, (3) interrupt logic
namespace Playcraft
{
    public class GetPercentOverTimeMono : MonoBehaviour
    {
        [SerializeField] ProgressEvent progress;
        [SerializeField] float duration;
        [SerializeField] FloatEvent Percent;
        [SerializeField] bool allowInterrupt = true;
        
        GetPercentOverTime timer = new GetPercentOverTime();
        
        public void SetDurationAndBegin(float value)
        {
            SetDuration(value);
            Begin();
        }
        
        public void SetDuration(float value) { duration = value; }
        
        public void Begin() 
        {
            if (!timer.inProgress || allowInterrupt)
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
                Percent.Invoke(timer.percent);
            }
        }
    }
}
