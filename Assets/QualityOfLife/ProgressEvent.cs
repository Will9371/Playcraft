using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProgressEvent : MonoBehaviour 
{
    public PercentEvent[] actions;
    
    #pragma warning disable 0649
    [SerializeField] bool overrideInputValidation;
    #pragma warning restore 0649
    
    // Enforce valid input: events organized by increasing percent threshold
    private void OnValidate()
    {
        if (overrideInputValidation)
            return;
    
        for (int i = 0; i < actions.Length - 1; i++)
            if (actions[i].percent > actions[i + 1].percent)
                actions[i].percent = actions[i + 1].percent;
    }
    
    public void Begin(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(EventTimer(duration)); 
    }
    
    public void Cancel()
    {
        StopAllCoroutines();
    }
    
    IEnumerator EventTimer(float fullDuration)
    {
        //Debug.Log("Event series beginning");
        for (int i = 0; i < actions.Length; i++)
        {
            var priorPercent = i == 0 ? 0f : actions[i - 1].percent;
            var delayPercent = actions[i].percent - priorPercent;
            var duration = fullDuration * delayPercent;
            
            //Debug.Log("Waiting for " + duration.ToString("F2"));
            yield return new WaitForSeconds(duration);
            actions[i].OnReached.Invoke();
        }
        //Debug.Log("Event series ending");
    }
    
    private void OnDisable()
    {
        Cancel();
    }
}

[Serializable]
public struct PercentEvent
{
    [Range(0f, 1f)] public float percent;
    public UnityEvent OnReached;
}
