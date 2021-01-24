using System.Collections;
using UnityEngine;
using Playcraft;


public class GetPercentOverTime : MonoBehaviour
{
    [SerializeField] ProgressEvent progress;
    [SerializeField] float duration;
    [SerializeField] FloatEvent Percent;
    
    public void SetDuration(float value) { duration = value; }
    
    public void Begin() { StartCoroutine(Process()); }
    
    IEnumerator Process()
    {
        if (progress) progress.Begin(duration);
    
        float startTime = Time.time;
        float percent = 0f;
        float elapsedTime = 0f;
        
        while (percent < 1f)
        {
            yield return null;
            elapsedTime = Time.time - startTime;
            percent = elapsedTime/duration;
            Percent.Invoke(percent);
        }        
    }
}
