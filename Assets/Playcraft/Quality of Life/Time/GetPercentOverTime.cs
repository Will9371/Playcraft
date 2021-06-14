using System.Collections;
using UnityEngine;
using Playcraft;


public class GetPercentOverTime : MonoBehaviour
{
    [SerializeField] ProgressEvent progress;
    [SerializeField] float duration;
    [SerializeField] FloatEvent Percent;
    [SerializeField] bool allowInterrupt = true;
    
    bool inProcess;
    
    public void SetDuration(float value) { duration = value; }
    
    public void Begin() 
    {
        if (!inProcess || allowInterrupt) 
            StartCoroutine(Process()); 
    }
    
    IEnumerator Process()
    {
        inProcess = true;
        
        if (progress) progress.Begin(duration);
    
        float startTime = Time.time;
        float percent = 0f;
        float elapsedTime;
        
        while (percent < 1f)
        {
            yield return null;
            elapsedTime = Time.time - startTime;
            percent = elapsedTime/duration;
            Percent.Invoke(percent);
        }
        
        inProcess = false;        
    }
}
