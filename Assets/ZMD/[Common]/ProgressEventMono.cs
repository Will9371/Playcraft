using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class ProgressEventMono : MonoBehaviour 
    {
        public ProgressEvent process;
        
        void OnValidate() { process.OnValidate(this); }
        void OnDisable() { Cancel(); }

        public void SetDurationAndBegin(float newDuration) { process.SetDurationAndBegin(newDuration); }
        public void Begin() { process.Begin(); }
        public void Cancel() { process.Cancel(); }
    }
    
    
    /// Invoke UnityEvents at points on a timer based on percent complete
    [Serializable]
    public class ProgressEvent
    {
        MonoBehaviour mono;
    
        public float duration = 1f;
        public PercentEvent[] actions;
        [SerializeField] bool overrideInputValidation;
                
        // Enforce valid input: events organized by increasing percent threshold
        public void OnValidate(MonoBehaviour mono)
        {
            this.mono = mono;
        
            if (overrideInputValidation)
                return;
        
            for (int i = 0; i < actions.Length - 1; i++)
                if (actions[i].percent > actions[i + 1].percent)
                    actions[i].percent = actions[i + 1].percent;
        }
        
        public void SetDurationAndBegin(float newDuration)
        {
            duration = newDuration;
            Begin();
        }
        
        public void Begin()
        {
            //Debug.Log($"ProgressEvent.Begin from {gameObject.name} {Time.time}", gameObject);
            mono.StopAllCoroutines();
            mono.StartCoroutine(EventTimer(duration));             
        }
        
        IEnumerator EventTimer(float fullDuration)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                var priorPercent = i == 0 ? 0f : actions[i - 1].percent;
                var delayPercent = actions[i].percent - priorPercent;
                var duration = fullDuration * delayPercent;
                
                yield return new WaitForSeconds(duration);
                actions[i].OnReached.Invoke();
            }
        }
        
        public void Cancel() { mono.StopAllCoroutines(); }
        void OnDisable() { Cancel(); }        
    }

    [Serializable]
    public struct PercentEvent
    {
        [Range(0f, 1f)] public float percent;
        public UnityEvent OnReached;
    }
}
