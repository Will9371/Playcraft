using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Invoke UnityEvents at points on a timer based on percent complete
// Cannot delegate to POCO because uses Coroutine, which relies on MonoBehaviour
namespace Playcraft
{
    public class ProgressEvent : MonoBehaviour 
    {
        public PercentEvent[] actions;
        [SerializeField] bool overrideInputValidation = default;
                
        // Enforce valid input: events organized by increasing percent threshold
        void OnValidate()
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
        
        public void Cancel() { StopAllCoroutines(); }
        void OnDisable() { Cancel(); }
    }

    [Serializable]
    public struct PercentEvent
    {
        [Range(0f, 1f)] public float percent;
        public UnityEvent OnReached;
    }
}
